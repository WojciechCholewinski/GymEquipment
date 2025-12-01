using GymEquipment.Api.Contracts;
using GymEquipment.Domain.History;
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

        [Fact]
        public async Task CreateProduct_WithForbiddenPhraseInName_Returns422()
        {
            // Arrange – fraza "scam" jako zakazana
            var request = new CreateProductRequest(
                Name: "SuperScamBarbell",
                Description: "Test product with forbidden phrase",
                Type: EquipmentType.Barbell,
                Category: ProductCategory.StrengthEquipment,
                WeightKg: 20m,
                QuantityAvailable: 5,
                Price: 400m
            );

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", request);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Product.Name.ContainsForbiddenPhrase", content);
        }

        [Fact]
        public async Task CreateProduct_BarbellWithoutWeight_Returns422()
        {
            // Arrange – typ Barbell, ale WeightKg = null
            var request = new CreateProductRequest(
                Name: "BarbellNoWeight",
                Description: "Barbell without weight",
                Type: EquipmentType.Barbell,
                Category: ProductCategory.StrengthEquipment,
                WeightKg: null,
                QuantityAvailable: 5,
                Price: 400m
            );

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", request);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            // możesz sprawdzić pole lub kod błędu – zależnie jak budujesz ValidationProblemDetails
            Assert.Contains("WeightKg", content);
        }

        [Fact]
        public async Task UpdateProduct_CreatesHistoryEntry()
        {
            // Arrange – najpierw tworzymy poprawny produkt
            var createRequest = new CreateProductRequest(
                Name: "HistoryBarbell20kg",
                Description: "Barbell for history test",
                Type: EquipmentType.Barbell,
                Category: ProductCategory.StrengthEquipment,
                WeightKg: 20m,
                QuantityAvailable: 10,
                Price: 500m
            );

            var createResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
            createResponse.EnsureSuccessStatusCode();

            var created = await createResponse.Content.ReadFromJsonAsync<ProductDto>();
            Assert.NotNull(created);
            var id = created!.Id;

            // Act – aktualizujemy cenę i ilość
            var updateRequest = new UpdateProductRequest(
                Name: created.Name,
                Description: created.Description,
                WeightKg: created.WeightKg,
                QuantityAvailable: created.QuantityAvailable + 5,
                Price: created.Price + 100m
            );

            var updateResponse = await _client.PutAsJsonAsync($"/api/products/{id}", updateRequest);
            updateResponse.EnsureSuccessStatusCode();

            // Assert – sprawdzamy historię
            var historyResponse = await _client.GetAsync($"/api/products/{id}/history");
            historyResponse.EnsureSuccessStatusCode();

            var history = await historyResponse.Content.ReadFromJsonAsync<IEnumerable<ProductHistoryEntryDto>>();
            Assert.NotNull(history);

            var entries = history!.ToList();
            Assert.NotEmpty(entries);
            Assert.Contains(entries, e => e.ChangeType == ProductChangeType.Updated);
        }
    }
}
