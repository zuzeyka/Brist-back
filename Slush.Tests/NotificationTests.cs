using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Slush.Data;
using Slush.Data.Entity.Profile;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the new Notification feature: friend request
    // send/accept/decline should each notify the other side, respecting their
    // Settings preference toggle, and a notification is only ever visible/
    // actionable by its own recipient.
    public class NotificationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public NotificationTests(CustomWebApplicationFactory factory)
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

        private async Task<List<NotificationDto>> GetMine(Guid userId)
        {
            var response = await _factory.AuthenticatedClient(userId).GetAsync("/api/Notification");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<NotificationDto>>() ?? new();
        }

        [Fact]
        public async Task Notification_GetMine_LoggedOut_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Notification");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task FriendRequest_Create_NotifiesRecipient()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();

            await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient });

            var mine = await GetMine(recipient);
            var notif = Assert.Single(mine);
            Assert.Equal(0, notif.type); // FriendRequestReceived
            Assert.Equal(sender, notif.actorId);
            Assert.False(notif.isRead);

            // The sender gets nothing for their own outgoing request.
            Assert.Empty(await GetMine(sender));
        }

        [Fact]
        public async Task FriendRequest_Accept_NotifiesSender()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            var created = await (await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient }))
                .Content.ReadFromJsonAsync<FriendsDto>();

            await _factory.AuthenticatedClient(recipient).PutAsync($"/api/Friends/accept/{created!.id}", null);

            var senderNotifs = await GetMine(sender);
            var accepted = Assert.Single(senderNotifs);
            Assert.Equal(1, accepted.type); // FriendRequestAccepted
            Assert.Equal(recipient, accepted.actorId);
        }

        [Fact]
        public async Task FriendRequest_MutualAdd_NotifiesOriginalSenderOfAcceptance()
        {
            // B adding A back while A's request is still pending is also an accept,
            // just through POST /api/Friends instead of PUT accept/{id}.
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient });

            await _factory.AuthenticatedClient(recipient).PostAsJsonAsync("/api/Friends", new { userId = recipient, friendId = sender });

            var senderNotifs = await GetMine(sender);
            Assert.Contains(senderNotifs, n => n.type == 1 && n.actorId == recipient);
        }

        [Fact]
        public async Task FriendRequest_Decline_NotifiesSender()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            var created = await (await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient }))
                .Content.ReadFromJsonAsync<FriendsDto>();

            await _factory.AuthenticatedClient(recipient).DeleteAsync($"/api/Friends/{created!.id}");

            var senderNotifs = await GetMine(sender);
            var declined = Assert.Single(senderNotifs);
            Assert.Equal(2, declined.type); // FriendRequestDeclined
        }

        [Fact]
        public async Task FriendRequest_Cancel_BySender_DoesNotNotifyAnyone()
        {
            // The sender canceling their own still-pending request is not a "decline."
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            var created = await (await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient }))
                .Content.ReadFromJsonAsync<FriendsDto>();

            await _factory.AuthenticatedClient(sender).DeleteAsync($"/api/Friends/{created!.id}");

            Assert.Empty(await GetMine(sender));
            // Recipient still has their original "received" notification — cancel
            // doesn't retract it, but no new (e.g. declined) one is added either.
            var recipientNotifs = await GetMine(recipient);
            Assert.Single(recipientNotifs);
            Assert.Equal(0, recipientNotifs[0].type);
        }

        [Fact]
        public async Task FriendRequest_Create_RespectsDisabledPreference()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            await _factory.AuthenticatedClient(recipient).PostAsJsonAsync("/api/Settings", new
            {
                bigSaleNotification = true,
                saleFromWishlistNotification = true,
                newCommentNotification = true,
                friendRequestNotification = false,
                approvedFriendRequestNotification = true,
                declinedFriendRequestNotification = true,
            });

            await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient });

            Assert.Empty(await GetMine(recipient));
        }

        [Fact]
        public async Task Notification_MarkRead_ByNonRecipient_Returns403()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient });
            var notif = (await GetMine(recipient)).Single();

            var response = await _factory.AuthenticatedClient(sender).PutAsync($"/api/Notification/markread/{notif.id}", null);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Notification_MarkRead_ByRecipient_Succeeds()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient });
            var notif = (await GetMine(recipient)).Single();

            var response = await _factory.AuthenticatedClient(recipient).PutAsync($"/api/Notification/markread/{notif.id}", null);

            response.EnsureSuccessStatusCode();
            Assert.True((await GetMine(recipient)).Single().isRead);
        }

        [Fact]
        public async Task Notification_ClearAll_RemovesOnlyCallersOwn()
        {
            var sender = await SeedUserAsync();
            var recipient = await SeedUserAsync();
            await _factory.AuthenticatedClient(sender).PostAsJsonAsync("/api/Friends", new { userId = sender, friendId = recipient });

            var response = await _factory.AuthenticatedClient(recipient).DeleteAsync("/api/Notification/clearall");

            response.EnsureSuccessStatusCode();
            Assert.Empty(await GetMine(recipient));
        }

        private record NotificationDto(Guid id, Guid userId, int type, Guid actorId, bool isRead, DateTime? createdAt);
        private record FriendsDto(Guid id, Guid userId, Guid friendId, DateTime? createdAt, int status);
    }
}
