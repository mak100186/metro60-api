using System.Net;

using Metro60.Core.Models;
using Metro60.FunctionalTests.Support;

using Newtonsoft.Json;

using RestSharp;

namespace Metro60.FunctionalTests.StepDefinitions;
[Binding]
public sealed class ProductStepDefinitions : TestBase
{
    public ProductStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext) { }

    [Given("I have products in the system")]
    public void GivenIHaveProductsInTheSystem()
    {
        //ensure the service is running as the functional tests require an instance of the running service,
        //see readme for instructions on how to run functional tests.

        Response = null;
    }

    [When("I update the following product:")]
    public async Task WhenIUpdateTheFollowingProduct(Table table)
    {
        var request = UpdateProductRequest(table);
        Response = await Client.ExecutePutAsync(request);
    }

    [When("I add the following product:")]
    public async Task WhenIAddTheFollowingProduct(Table table)
    {
        var request = CreateProductRequest(table);
        Response = await Client.ExecutePostAsync(request);
    }

    [When("I update the following product without authentication:")]
    public async Task WhenIUpdateTheFollowingProductWithoutAuthentication(Table table)
    {
        var request = UpdateProductRequest(table);
        Response = await ClientWithoutAuthentication.ExecutePutAsync(request);
    }

    [When("I add the following product without authentication:")]
    public async Task WhenIAddTheFollowingProductWithoutAuthentication(Table table)
    {
        var request = CreateProductRequest(table);
        Response = await ClientWithoutAuthentication.ExecutePostAsync(request);
    }

    [When("I request all products")]
    public async Task WhenIRequestAllProducts()
    {
        var request = new RestRequest("product");
        Response = await Client.ExecuteGetAsync(request);
    }

    [When("I request a product with id: (.*)")]
    public async Task WhenIRequestAProductWithId(int productId)
    {
        var request = new RestRequest($"product/{productId}");
        Response = await Client.ExecuteGetAsync(request);
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int statusCode)
    {
        var expectedStatusCode = (HttpStatusCode)statusCode;

        Response.Should().NotBeNull();
        Response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Then("the result should be (.*) with the following errors:")]
    public void ThenTheResultShouldBeWithTheFollowingErrors(int statusCode, Table table)
    {
        var expectedStatusCode = (HttpStatusCode)statusCode;

        Response.Should().NotBeNull();
        Response.StatusCode.Should().Be(expectedStatusCode);

        var errors = JsonConvert.DeserializeObject<ErrorResponse>(Response.Content);
        errors.Should().NotBeNull();

        foreach (var tableRow in table.Rows)
        {
            var key = tableRow["key"];
            var value = tableRow["value"];

            if (errors.Type == key)
            {
                errors.Message.Should().Be(value);
            }
            else
            {
                var errorForTheKey = errors.Errors[key];
                errorForTheKey.Should().Contain(value);
            }
        }
    }

    [Then("I should get (.*) products")]
    public void ThenIShouldGetProducts(int count)
    {
        var content = JsonConvert.DeserializeObject<List<ProductModel>>(Response.Content);
        content.Should().NotBeNullOrEmpty();
        content.Should().HaveCount(count);
    }

    [Then("I should get the product with the following:")]
    public void ThenIShouldGetProducts(Table table)
    {
        var content = JsonConvert.DeserializeObject<ProductModel>(Response.Content);
        content.Should().NotBeNull();

        table.RowCount.Should().Be(1, "This step can only work with a single row");
        var row = table.Rows.Single();

        content.Title.Should().Be(row[nameof(ProductModel.Title)]);
        content.Description.Should().Be(row[nameof(ProductModel.Description)]);
        content.Price.Should().Be(double.Parse(row[nameof(ProductModel.Price)]));
        content.DiscountPercentage.Should().Be(double.Parse(row[nameof(ProductModel.DiscountPercentage)]));
        content.Rating.Should().Be(double.Parse(row[nameof(ProductModel.Rating)]));
        content.Stock.Should().Be(int.Parse(row[nameof(ProductModel.Stock)]));
        content.Brand.Should().Be(row[nameof(ProductModel.Brand)]);
        content.Category.Should().Be(row[nameof(ProductModel.Category)]);
        content.Thumbnail.Should().Be(row[nameof(ProductModel.Thumbnail)]);
    }

    private RestRequest CreateProductRequest(Table table)
    {
        table.RowCount.Should().Be(1, "This step can only work with a single row");
        var row = table.Rows.Single();

        var request = new RestRequest("product")
        {
            RequestFormat = DataFormat.Json,
            Method = Method.Post
        };
        request.AddJsonBody(new ProductModel
        {
            Id = int.Parse(row[nameof(ProductModel.Id)]),
            Title = row[nameof(ProductModel.Title)],
            Description = row[nameof(ProductModel.Description)],
            Price = double.Parse(row[nameof(ProductModel.Price)]),
            DiscountPercentage = double.Parse(row[nameof(ProductModel.DiscountPercentage)]),
            Rating = double.Parse(row[nameof(ProductModel.Rating)]),
            Stock = int.Parse(row[nameof(ProductModel.Stock)]),
            Brand = row[nameof(ProductModel.Brand)],
            Category = row[nameof(ProductModel.Category)],
            Thumbnail = row[nameof(ProductModel.Thumbnail)],
            Images = row[nameof(ProductModel.Images)].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
        });
        return request;
    }

    private RestRequest UpdateProductRequest(Table table)
    {
        table.RowCount.Should().Be(1, "This step can only work with a single row");
        var row = table.Rows.Single();

        var request = new RestRequest($"product/{int.Parse(row[nameof(ProductModel.Id)])}")
        {
            RequestFormat = DataFormat.Json,
            Method = Method.Put
        };
        request.AddJsonBody(new ProductModel
        {
            Id = int.Parse(row[nameof(ProductModel.Id)]),
            Title = row[nameof(ProductModel.Title)],
            Description = row[nameof(ProductModel.Description)],
            Price = double.Parse(row[nameof(ProductModel.Price)]),
            DiscountPercentage = double.Parse(row[nameof(ProductModel.DiscountPercentage)]),
            Rating = double.Parse(row[nameof(ProductModel.Rating)]),
            Stock = int.Parse(row[nameof(ProductModel.Stock)]),
            Brand = row[nameof(ProductModel.Brand)],
            Category = row[nameof(ProductModel.Category)],
            Thumbnail = row[nameof(ProductModel.Thumbnail)],
            Images = row[nameof(ProductModel.Images)].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
        });
        return request;
    }
}
