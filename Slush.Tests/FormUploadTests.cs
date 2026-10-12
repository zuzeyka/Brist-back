using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Slush.Tests
{
    // Regression coverage for the [FromBody] + IFormFile combination that caused
    // a 415 Unsupported Media Type on every real multipart request to these
    // endpoints — fixed by switching the model parameter to [FromForm].
    public class FormUploadTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public FormUploadTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreatePublisher_MultipartFormWithoutFiles_DoesNotReturn415()
        {
            var client = _factory.CreateClient();
            using var form = new MultipartFormDataContent
            {
                { new StringContent("Test Publisher"), "name" },
                { new StringContent("A publisher created by a test"), "description" },
            };

            var response = await client.PostAsync("/api/Publisher", form);

            Assert.NotEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<PublisherDto>();
            Assert.Equal("Test Publisher", created!.name);
        }

        [Fact]
        public async Task CreatePublisher_MultipartFormWithAvatarFile_BindsBothModelAndFile()
        {
            var client = _factory.CreateClient();
            using var form = new MultipartFormDataContent
            {
                { new StringContent("Publisher With Avatar"), "name" },
            };
            var fileContent = new ByteArrayContent(new byte[] { 1, 2, 3, 4 });
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            form.Add(fileContent, "avatar", "avatar.png");

            var response = await client.PostAsync("/api/Publisher", form);

            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<PublisherDto>();
            Assert.Equal("Publisher With Avatar", created!.name);
            Assert.False(string.IsNullOrEmpty(created.avatar));
        }

        [Fact]
        public async Task CreatePost_MultipartFormWithoutFile_DoesNotReturn415()
        {
            var client = _factory.CreateClient();
            using var form = new MultipartFormDataContent
            {
                { new StringContent(Guid.NewGuid().ToString()), "discussionId" },
                { new StringContent("Test post title"), "title" },
                { new StringContent("Test post content"), "content" },
            };

            var response = await client.PostAsync("/api/Post", form);

            Assert.NotEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        private record PublisherDto(Guid id, string? name, string? avatar);
    }
}
