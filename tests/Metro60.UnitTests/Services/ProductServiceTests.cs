using FluentAssertions;

using Metro60.Core.Data;
using Metro60.Core.Entities;
using Metro60.Core.Extensions;
using Metro60.Core.Models;
using Metro60.Core.Services;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace Metro60.UnitTests.Services;

[TestFixture]
[Category("ProductService")]
public class ProductServiceTests
{
    private TestScope _scope;
    private List<Product> _products;

    [SetUp]
    public void Setup()
    {
        _products = new List<Product>
        {
            new()
            {
                Id = 100,
                Brand = "Brand Name 100",
                Title = "Title of the product 100",
                Category = "Category of the product",
                Description = "Description of the product",
                DiscountPercentage = 2.0f,
                Price = 3.0f,
                Rating = 4.0f,
                Stock = 5,
                Thumbnail = "some uri",
                Images = string.Join(',', new List<string> { "uri 1", "http://someurl.com", "ftp://10.0.1.2" })
            },
            new()
            {
                Id = 200,
                Brand = "Brand Name 200",
                Title = "Title of the product 200",
                Category = "Category of the product",
                Description = "Description of the product",
                DiscountPercentage = 2.0f,
                Price = 3.0f,
                Rating = 4.0f,
                Stock = 5,
                Thumbnail = "some uri",
                Images = string.Join(',', new List<string> { "uri 1", "http://someurl.com", "ftp://10.0.1.2" })
            }
        };
        _scope = new TestScope();
        _scope.Context.Products.AddRange(_products);
        _scope.Context.SaveChanges();
    }

    [Test]
    public async Task AddProduct_ReturnsAllExistingProductsAlongWithTheNewlyAddedOne_WhenCalled()
    {
        //act
        var productModels = await _scope.ServiceUnderTest.GetAll();

        //assert
        var expectedCountBeforeAddition = _products.Count;
        productModels.Count.Should().Be(expectedCountBeforeAddition);

        var newProduct = new ProductModel
        {
            Id = 500,
            Brand = "Brand Name 500",
            Title = "Title of the product 500",
            Category = "Category of the product",
            Description = "Description of the product",
            DiscountPercentage = 2.0f,
            Price = 3.0f,
            Rating = 4.0f,
            Stock = 5,
            Thumbnail = "some uri",
            Images =new List<string> { "uri 1", "http://someurl.com", "ftp://10.0.1.2" }
        };

        //act
        await _scope.ServiceUnderTest.AddProduct(newProduct);

        //assert
        productModels = await _scope.ServiceUnderTest.GetAll();
        productModels.Count.Should().Be(expectedCountBeforeAddition + 1);

        var product = await _scope.ServiceUnderTest.Get(500);
        product.Should().NotBeNull();
        product.Id.Should().Be(newProduct.Id);
        product.Brand.Should().Be(newProduct.Brand);
        product.Title.Should().Be(newProduct.Title);
        
    }

    [TestCase(100, "Brand Name 100", "Title of the product 100", true)]
    [TestCase(100, null, "Title of the product [Different from original]", false)]
    public async Task AddProduct_ThrowsArgumentException_WhenCalledWithExistingBrandOrTitle(int id, string newBrandName, string newTitleName, bool shouldThrow)
    {
        //act
        var productModel = await _scope.ServiceUnderTest.Get(id);
        productModel.Should().NotBeNull();
        if (!string.IsNullOrWhiteSpace(newBrandName))
        {
            productModel.Brand = newBrandName;
        }

        if (!string.IsNullOrWhiteSpace(newTitleName))
        {
            productModel.Title = newTitleName;
        }

        //assert
        if (shouldThrow)
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _scope.ServiceUnderTest.AddProduct(productModel);
            }, $"Product with Brand={productModel.Brand} and Title={productModel.Title} already exists.");
        }
        else
        {
            productModel.Id = 600;
            var newlyAddedProduct = await _scope.ServiceUnderTest.AddProduct(productModel);
            newlyAddedProduct.Should().NotBeNull();
            newlyAddedProduct.Id.Should().Be(600);
        }
    }

    [Test]
    public async Task GetAll_ReturnsAllProducts_WhenCalled()
    {
        //act
        var productModels = await _scope.ServiceUnderTest.GetAll();

        //assert
        productModels.Count.Should().Be(_products.Count);

        foreach (var productModel in productModels)
        {
            var product = _products.SingleOrDefault(x => x.Id == productModel.Id);

            product.Should().NotBeNull();
            product.Id.Should().Be(productModel.Id);
            product.Brand.Should().Be(productModel.Brand);
            product.Title.Should().Be(productModel.Title);
        }
    }

    [TestCase(100, false, "Title of the product 100")]
    [TestCase(300, true, null)]
    public async Task Get_ReturnsSpecificUsers_WhenCalled(int id, bool expectNull, string titleContent)
    {
        //act
        var productModels = await _scope.ServiceUnderTest.Get(id);

        //assert
        if (expectNull)
        {
            productModels.Should().BeNull();
        }
        else
        {
            productModels.Should().NotBeNull();
            productModels.Title.Should().Be(titleContent);
        }
    }

    [TestCase(100, "Updated title")]
    [TestCase(400, null)]
    public async Task UpdateProduct_UpdatesSpecifiedProducts_WhenCalled(int id, string titleContent)
    {
        //arrange
        var productToUpdate = _products.FirstOrDefault(x => x.Id == id);
        if (productToUpdate != null)
        {
            productToUpdate.Title = titleContent;
        }
        
        //act & assert
        if (string.IsNullOrWhiteSpace(titleContent))
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _scope.ServiceUnderTest.UpdateProduct(id, productToUpdate?.ToDto());
            }, "Given Id not found.", nameof(id));
        }
        else
        {
            var productModels = await _scope.ServiceUnderTest.UpdateProduct(id, productToUpdate?.ToDto());

            productModels.Should().NotBeNull();

            //note to reviewer: json based comparison. json fields are ordered
            var serializedActual = JsonConvert.SerializeObject(productModels);
            var serializedExpected = JsonConvert.SerializeObject(productToUpdate);

            serializedExpected.Should().Be(serializedActual);
        }
    }

    private class TestScope
    {
        internal MetroDbContext Context { get; set; }
        internal ProductService ServiceUnderTest { get; init; }

        public TestScope()
        {
            var dbContext = new MetroDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: $"TestDatabase{Guid.NewGuid()}")
                .Options);

            Context = dbContext;
            ServiceUnderTest = new ProductService(dbContext);

        }
    }
}
