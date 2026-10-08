using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the ownership-check pass over Friends, Chat and
    // Message — previously any caller, logged in or not, could read every
    // relationship/conversation in the system and create/update/delete any other
    // user's friend relations, chats or messages by guessing ids.
    public class FriendsChatMessageOwnershipTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public FriendsChatMessageOwnershipTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        private async Task<Guid> SeedUserAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var user = new User(Guid.NewGuid(), "test", "x", null, null, null, true, 0, 0, DateTime.UtcNow);
            context.dbUsers.Add(user);
            await context.SaveChangesAsync();
            return user.id;
        }

        // --- Friends ---

        [Fact]
        public async Task Friends_OldBareGetAll_NoLongerExists()
        {
            var client = _factory.AuthenticatedClient(await SeedUserAsync());

            var response = await client.GetAsync("/api/Friends");

            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Friends_GetByUserId_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Friends/getbyuserid/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Friends_GetByUserId_AsAnyAuthenticatedUser_IsPublic()
        {
            // Unlike Settings/OwnedGame, a friends list is shown on every profile page,
            // so any authenticated caller — not just the owner — may read it.
            var ownerId = await SeedUserAsync();
            var viewerId = await SeedUserAsync();
            var viewerClient = _factory.AuthenticatedClient(viewerId);

            var response = await viewerClient.GetAsync($"/api/Friends/getbyuserid/{ownerId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Friends_Create_IgnoresSpoofedUserId()
        {
            var realUserId = await SeedUserAsync();
            var spoofedUserId = await SeedUserAsync();
            var otherSide = await SeedUserAsync();
            var client = _factory.AuthenticatedClient(realUserId);

            var response = await client.PostAsJsonAsync("/api/Friends", new
            {
                userId = spoofedUserId,
                friendId = otherSide,
            });

            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<FriendsDto>();
            Assert.Equal(realUserId, created!.userId);
        }

        [Fact]
        public async Task Friends_Create_StartsAsPending_AndIsExcludedFromFriendsList()
        {
            var userId = await SeedUserAsync();
            var friendId = await SeedUserAsync();
            var client = _factory.AuthenticatedClient(userId);

            var created = await (await client.PostAsJsonAsync("/api/Friends", new { userId, friendId }))
                .Content.ReadFromJsonAsync<FriendsDto>();

            Assert.Equal(0, created!.status);

            var list = await (await client.GetAsync($"/api/Friends/getbyuserid/{userId}"))
                .Content.ReadFromJsonAsync<List<FriendsDto>>();
            Assert.Empty(list!);
        }

        [Fact]
        public async Task Friends_Create_WhenReverseRequestIsPending_AutoAcceptsInstead()
        {
            var userA = await SeedUserAsync();
            var userB = await SeedUserAsync();

            // A sends a request to B first.
            await _factory.AuthenticatedClient(userA).PostAsJsonAsync("/api/Friends", new { userId = userA, friendId = userB });

            // B "adds" A back — this should accept A's existing request, not create a second row.
            var result = await (await _factory.AuthenticatedClient(userB).PostAsJsonAsync("/api/Friends", new { userId = userB, friendId = userA }))
                .Content.ReadFromJsonAsync<FriendsDto>();
            Assert.Equal(1, result!.status);

            var aList = await (await _factory.AuthenticatedClient(userA).GetAsync($"/api/Friends/getbyuserid/{userA}"))
                .Content.ReadFromJsonAsync<List<FriendsDto>>();
            Assert.Single(aList!);
        }

        [Fact]
        public async Task Friends_Accept_BySender_Returns403()
        {
            var userId = await SeedUserAsync();
            var friendId = await SeedUserAsync();
            var created = await (await _factory.AuthenticatedClient(userId).PostAsJsonAsync("/api/Friends", new { userId, friendId }))
                .Content.ReadFromJsonAsync<FriendsDto>();

            // The sender cannot accept their own outgoing request.
            var response = await _factory.AuthenticatedClient(userId).PutAsync($"/api/Friends/accept/{created!.id}", null);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Friends_Accept_ByRecipient_Succeeds()
        {
            var userId = await SeedUserAsync();
            var friendId = await SeedUserAsync();
            var created = await (await _factory.AuthenticatedClient(userId).PostAsJsonAsync("/api/Friends", new { userId, friendId }))
                .Content.ReadFromJsonAsync<FriendsDto>();

            var response = await _factory.AuthenticatedClient(friendId).PutAsync($"/api/Friends/accept/{created!.id}", null);
            response.EnsureSuccessStatusCode();

            var list = await (await _factory.AuthenticatedClient(userId).GetAsync($"/api/Friends/getbyuserid/{userId}"))
                .Content.ReadFromJsonAsync<List<FriendsDto>>();
            Assert.Single(list!);
        }

        [Fact]
        public async Task Friends_Relationship_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Friends/relationship/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Friends_Delete_ByNonParticipant_Returns403()
        {
            var userId = await SeedUserAsync();
            var friendId = await SeedUserAsync();
            var ownerClient = _factory.AuthenticatedClient(userId);
            var created = await (await ownerClient.PostAsJsonAsync("/api/Friends", new
            {
                userId,
                friendId,
            })).Content.ReadFromJsonAsync<FriendsDto>();

            var attackerClient = _factory.AuthenticatedClient(await SeedUserAsync());
            var response = await attackerClient.DeleteAsync($"/api/Friends/{created!.id}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Friends_Delete_ByEitherParticipant_Succeeds()
        {
            var userId = await SeedUserAsync();
            var friendId = await SeedUserAsync();
            var ownerClient = _factory.AuthenticatedClient(userId);
            var created = await (await ownerClient.PostAsJsonAsync("/api/Friends", new
            {
                userId,
                friendId,
            })).Content.ReadFromJsonAsync<FriendsDto>();

            // The non-creating side of the relation can also delete it.
            var friendSideClient = _factory.AuthenticatedClient(friendId);
            var response = await friendSideClient.DeleteAsync($"/api/Friends/{created!.id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        // --- Chat ---

        [Fact]
        public async Task Chat_OldBareGetAll_NoLongerExists()
        {
            var client = _factory.AuthenticatedClient(await SeedUserAsync());

            var response = await client.GetAsync("/api/Chat");

            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Chat_Create_ByNonParticipant_Returns403()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var attackerClient = _factory.AuthenticatedClient(await SeedUserAsync());

            var response = await attackerClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            });

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Chat_Create_Twice_ReturnsSameChat()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var client = _factory.AuthenticatedClient(firstUser);

            var first = await (await client.PostAsJsonAsync("/api/Chat", new { firstUser, secondUser }))
                .Content.ReadFromJsonAsync<ChatDto>();
            var second = await (await client.PostAsJsonAsync("/api/Chat", new { firstUser, secondUser }))
                .Content.ReadFromJsonAsync<ChatDto>();

            Assert.Equal(first!.id, second!.id);

            var listResponse = await client.GetAsync($"/api/Chat/byuserid/{firstUser}");
            var list = await listResponse.Content.ReadFromJsonAsync<List<ChatDto>>();
            Assert.Single(list!);
        }

        [Fact]
        public async Task Chat_Get_ByNonParticipant_Returns403()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var participantClient = _factory.AuthenticatedClient(firstUser);
            var created = await (await participantClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            var attackerClient = _factory.AuthenticatedClient(await SeedUserAsync());
            var response = await attackerClient.GetAsync($"/api/Chat/{created!.id}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Chat_Get_ByParticipant_Returns200()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var participantClient = _factory.AuthenticatedClient(firstUser);
            var created = await (await participantClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            // secondUser never created the chat but is still a legitimate participant.
            var secondUserClient = _factory.AuthenticatedClient(secondUser);
            var response = await secondUserClient.GetAsync($"/api/Chat/{created!.id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Chat_GetByUserId_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Chat/byuserid/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Chat_GetByUserId_ForAnotherUser_Returns403()
        {
            var client = _factory.AuthenticatedClient(await SeedUserAsync());

            var response = await client.GetAsync($"/api/Chat/byuserid/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Chat_GetByUserId_ReturnsChatsFromEitherSide()
        {
            var me = await SeedUserAsync();
            var otherA = await SeedUserAsync();
            var otherB = await SeedUserAsync();
            var myClient = _factory.AuthenticatedClient(me);

            // One chat where I'm firstUser, one where I'm secondUser.
            await myClient.PostAsJsonAsync("/api/Chat", new { firstUser = me, secondUser = otherA });
            await _factory.AuthenticatedClient(otherB).PostAsJsonAsync("/api/Chat", new { firstUser = otherB, secondUser = me });

            var response = await myClient.GetAsync($"/api/Chat/byuserid/{me}");

            response.EnsureSuccessStatusCode();
            var chats = await response.Content.ReadFromJsonAsync<List<ChatDto>>();
            Assert.Equal(2, chats!.Count);
            Assert.All(chats, c => Assert.True(c.firstUser == me || c.secondUser == me));
        }

        // --- Message ---

        [Fact]
        public async Task Message_GetAllByChat_ByNonParticipant_Returns403()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var chatClient = _factory.AuthenticatedClient(firstUser);
            var chat = await (await chatClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            var attackerClient = _factory.AuthenticatedClient(await SeedUserAsync());
            var response = await attackerClient.GetAsync($"/api/Message/bychat/{chat!.id}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Message_GetAllByChat_UsesRouteId_NotBody()
        {
            // Regression for the old [FromBody] Chat chat parameter on a GET route,
            // which ignored the route's {id} entirely.
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var chatClient = _factory.AuthenticatedClient(firstUser);
            var chat = await (await chatClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            var response = await chatClient.GetAsync($"/api/Message/bychat/{chat!.id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Message_Create_ByNonParticipant_Returns403()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var chatClient = _factory.AuthenticatedClient(firstUser);
            var chat = await (await chatClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            var attackerClient = _factory.AuthenticatedClient(await SeedUserAsync());
            var response = await attackerClient.PostAsJsonAsync("/api/Message", new
            {
                chatId = chat!.id,
                senderId = firstUser,
                content = "hi",
            });

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Message_Create_IgnoresSpoofedSenderId()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var chatClient = _factory.AuthenticatedClient(firstUser);
            var chat = await (await chatClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            // secondUser sends a message but spoofs senderId as firstUser.
            var secondUserClient = _factory.AuthenticatedClient(secondUser);
            var response = await secondUserClient.PostAsJsonAsync("/api/Message", new
            {
                chatId = chat!.id,
                senderId = firstUser,
                content = "hi",
            });

            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<MessageDto>();
            Assert.Equal(secondUser, created!.senderId);
        }

        [Fact]
        public async Task Message_Delete_ByOtherParticipant_Returns403()
        {
            var firstUser = await SeedUserAsync();
            var secondUser = await SeedUserAsync();
            var chatClient = _factory.AuthenticatedClient(firstUser);
            var chat = await (await chatClient.PostAsJsonAsync("/api/Chat", new
            {
                firstUser,
                secondUser,
            })).Content.ReadFromJsonAsync<ChatDto>();

            var message = await (await chatClient.PostAsJsonAsync("/api/Message", new
            {
                chatId = chat!.id,
                senderId = firstUser,
                content = "hi",
            })).Content.ReadFromJsonAsync<MessageDto>();

            // secondUser is a legitimate chat participant but did not send this message.
            var secondUserClient = _factory.AuthenticatedClient(secondUser);
            var response = await secondUserClient.DeleteAsync($"/api/Message/{message!.id}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        private record FriendsDto(Guid id, Guid userId, Guid friendId, DateTime? createdAt, int status);
        private record ChatDto(Guid id, Guid firstUser, Guid secondUser, DateTime? createdAt);
        private record MessageDto(Guid id, Guid chatId, Guid senderId, string? content, DateTime? createdAt);
    }
}
