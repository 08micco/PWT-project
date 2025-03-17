using RichardSzalay.MockHttp;
using PWT_project.Client.Services;

namespace PWT_project.Tests
{
    public class VareBeholdningServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsData_WhenApiCallIsSuccessful()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            // The URL we expect the service to call:
            mockHttp.When("https://localhost:7140/api/varer-beholdning")
                    .Respond("application/json",
                     "[{\"EAN\":\"123456\",\"ItemDescription\":\"Test Item\",\"ColorCodeName\":\"Red\",\"InventoryQuantity\":10}]");

            // Our HttpClient uses the mock handler
            var httpClient = new HttpClient(mockHttp);

            // The service we want to test
            var service = new VareBeholdningService(httpClient);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("123456", result[0].EAN);
            Assert.Equal("Test Item", result[0].ItemDescription);
            Assert.Equal(10, result[0].InventoryQuantity);
        }

        [Fact]
        public async Task GetAllAsync_ThrowsHttpRequestException_WhenServerReturnsError()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            // Simulate an Internal Server Error (500)
            mockHttp.When("https://localhost:7140/api/varer-beholdning")
                    .Respond(System.Net.HttpStatusCode.InternalServerError);

            var httpClient = new HttpClient(mockHttp);
            var service = new VareBeholdningService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetAllAsync());
        }
    }
}






