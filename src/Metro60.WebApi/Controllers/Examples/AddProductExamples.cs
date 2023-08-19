using Metro60.Core.Models;

using Swashbuckle.AspNetCore.Filters;

namespace Metro60.WebApi.Controllers.Examples;

public class AddProductExamples : IMultipleExamplesProvider<ProductModel>
{
    public IEnumerable<SwaggerExample<ProductModel>> GetExamples()
    {
        yield return SwaggerExample.Create(
            "Example 1",
            "Example 1 - Product with multiple images",
            new ProductModel
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
                Thumbnail = "https://dummyjson.com/image/i/products/2/thumbnail.jpg",
                Images = new List<string> { "https://dummyjson.com/image/i/products/2/thumbnail.jpg", "https://dummyjson.com/image/i/products/2/thumbnail.jpg", "https://dummyjson.com/image/i/products/2/thumbnail.jpg" }
            });

        yield return SwaggerExample.Create(
            "Example 2",
            "Example 2 - Product with single image",
            new ProductModel
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
                Thumbnail = "https://dummyjson.com/image/i/products/2/thumbnail.jpg",
                Images = new List<string> { "https://dummyjson.com/image/i/products/2/thumbnail.jpg" }
            });
    }
}
