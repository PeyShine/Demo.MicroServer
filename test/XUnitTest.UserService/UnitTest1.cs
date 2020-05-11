using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest.UserService
{
    public class UnitTest1
    {
        private readonly HttpClient _client = null;
        private readonly string apigateway_url = "http://localhost:8000";

        public UnitTest1()
        {
            _client = new HttpClient();
        }

        [Fact]
        public async Task GetUserinfo_ShouldBe_Ok()
        {
            // Arrange
            var id = "6b3477bb-90fb-11ea-ad5f-0242ac110002";

            // Act
            var response = await _client.GetAsync($"{apigateway_url}/api/user/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
