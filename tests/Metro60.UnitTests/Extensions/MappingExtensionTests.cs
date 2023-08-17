using FluentAssertions;

using Metro60.Core.Entities;
using Metro60.Core.Extensions;
using Metro60.Core.Models;

using Newtonsoft.Json;

namespace Metro60.UnitTests.Extensions;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[Category("Mapping")]
public class Tests
{
    private List<string> _images;
    private User _userTestSubject;
    private Product _productTestSubject;
    private ProductModel _productModelTestSubject;

    [SetUp]
    public void Setup()
    {
        _userTestSubject = new User
        {
            Id = 1,
            FirstName = "Test User First Name",
            LastName = "Test User Last Name",
            Username = "Test Username",
            Password = "Test User Password"
        };

        _productModelTestSubject = new ProductModel
        {
            Id = 1,
            Brand = "Brand Name",
            Title = "Title of the product",
            Category = "Category of the product",
            Description = "Description of the product",
            DiscountPercentage = 2.0f,
            Price = 3.0f,
            Rating = 4.0f,
            Stock = 5,
            Thumbnail = "some uri",
            Images = new List<string> { "uri 1", "http://someurl.com", "ftp://10.0.1.2" }
        };

        _images = new List<string> { "uri 1", "http://someurl.com", "ftp://10.0.1.2" };
        _productTestSubject = new Product
        {
            Id = 1,
            Brand = "Brand Name",
            Title = "Title of the product",
            Category = "Category of the product",
            Description = "Description of the product",
            DiscountPercentage = 2.0f,
            Price = 3.0f,
            Rating = 4.0f,
            Stock = 5,
            Thumbnail = "some uri",
            Images = string.Join(',', _images)
        };
    }

    [Test]
    public void User_ToDto_ReturnsUserModelAsExpected()
    {
        //arrange & act
        var userModel = _userTestSubject.ToDto();

        //assert
        userModel.Should().NotBeNull();
        userModel.Id.Should().Be(1);
        userModel.FirstName.Should().Be(_userTestSubject.FirstName);
        userModel.LastName.Should().Be(_userTestSubject.LastName);
        userModel.Username.Should().Be(_userTestSubject.Username);
    }

    [Test]
    public void User_JsonConvert_ShouldNotSerializePassword()
    {
        //arrange & act
        var jsonString = JsonConvert.SerializeObject(_userTestSubject);

        //assert
        jsonString.Should().NotBeNull();
        jsonString.Should().Contain("Test User First Name");
        jsonString.Should().Contain("Test User Last Name");
        jsonString.Should().Contain("Test Username");
        jsonString.Should().NotContain("Test User Password");
    }

    [Test]
    public void ProductModel_ToDto_ReturnsProductAsExpected()
    {
        //arrange & act
        var product = _productModelTestSubject.ToDbo();

        //assert
        product.Should().NotBeNull();
        product.Brand.Should().Be(_productModelTestSubject.Brand);
        product.Title.Should().Be(_productModelTestSubject.Title);
        product.Category.Should().Be(_productModelTestSubject.Category);
        product.Description.Should().Be(_productModelTestSubject.Description);
        product.DiscountPercentage.Should().Be(_productModelTestSubject.DiscountPercentage);
        product.Price.Should().Be(_productModelTestSubject.Price);
        product.Stock.Should().Be(_productModelTestSubject.Stock);
        product.Rating.Should().Be(_productModelTestSubject.Rating);
        product.Thumbnail.Should().Be(_productModelTestSubject.Thumbnail);
        product.Images.Should().Be(string.Join(',', _productModelTestSubject.Images));
    }

    [Test]
    public void Product_ToDto_ReturnsProductModelAsExpected()
    {
        //arrange & act
        var productModel = _productTestSubject.ToDto();

        //assert
        productModel.Should().NotBeNull();
        productModel.Brand.Should().Be(_productTestSubject.Brand);
        productModel.Title.Should().Be(_productTestSubject.Title);
        productModel.Category.Should().Be(_productTestSubject.Category);
        productModel.Description.Should().Be(_productTestSubject.Description);
        productModel.DiscountPercentage.Should().Be(_productTestSubject.DiscountPercentage);
        productModel.Price.Should().Be(_productTestSubject.Price);
        productModel.Stock.Should().Be(_productTestSubject.Stock);
        productModel.Rating.Should().Be(_productTestSubject.Rating);
        productModel.Thumbnail.Should().Be(_productTestSubject.Thumbnail);
        productModel.Images.Should().BeEquivalentTo(_images);
    }

    [Test]
    public void Product_UpdateWith_ProductModelShouldUpdateExpectedFields()
    {
        //arrange & act
        _productTestSubject.UpdateWith(_productModelTestSubject);

        //assert
        _productTestSubject.Should().NotBeNull();
        _productTestSubject.Brand.Should().Be(_productModelTestSubject.Brand);
        _productTestSubject.Title.Should().Be(_productModelTestSubject.Title);
        _productTestSubject.Category.Should().Be(_productModelTestSubject.Category);
        _productTestSubject.Description.Should().Be(_productModelTestSubject.Description);
        _productTestSubject.DiscountPercentage.Should().Be(_productModelTestSubject.DiscountPercentage);
        _productTestSubject.Price.Should().Be(_productModelTestSubject.Price);
        _productTestSubject.Stock.Should().Be(_productModelTestSubject.Stock);
        _productTestSubject.Rating.Should().Be(_productModelTestSubject.Rating);
        _productTestSubject.Thumbnail.Should().Be(_productModelTestSubject.Thumbnail);
        _productTestSubject.Images.Should().Be(string.Join(',', _productModelTestSubject.Images));
    }
}
