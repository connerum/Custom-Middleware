using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using CustomMiddleware.Web.Middleware;

namespace CustomMiddleware.Tests
{
    public class AuthenticationMiddlewareTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AuthenticationMiddlewareTests()
        {
            _server = new TestServer(TestServer.CreateBuilder()
                .UseStartup<CustomMiddleware.Web.Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Should_Return_Unauthorized_Without_Credentials()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Unauthorized_With_Invalid_Credentials()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/?username=user5&password=password2");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_OK_With_Valid_Credentials()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/?username=user1&password=password1");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Unauthorized_With_Only_Username()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/?username=user1");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
