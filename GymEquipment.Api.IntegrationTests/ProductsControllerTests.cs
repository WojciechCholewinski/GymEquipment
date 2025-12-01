using GymEquipment.Api.Contracts;
using GymEquipment.Domain.Products;
using System.Net;
using System.Net.Http.Json;

namespace GymEquipment.Api.IntegrationTests
{
    public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateProduct_WithInvalidPrice_Returns422()
        {
            // Arrange
            var request = new CreateProductRequest(
                Name: "BadProduct123",
                Description: "Test product",
                Type: EquipmentType.Barbell,
                Category: ProductCategory.StrengthEquipment,
                WeightKg: 20m,
                QuantityAvailable: 5,
                Price: -10m // niepoprawna cena
            );

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", request);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Price", content);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            Assert.NotNull(products);
        }
    }
}
