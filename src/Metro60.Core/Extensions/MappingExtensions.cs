using Metro60.Core.Entities;
using Metro60.Core.Models;

namespace Metro60.Core.Extensions;

public static class MappingExtensions
{
    public static UserModel ToDto(this User model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username
        };

    public static Product ToDbo(this ProductModel model) =>
        new()
        {
            Id = model.Id,
            Brand = model.Brand,
            Category = model.Category,
            Description = model.Description,
            DiscountPercentage = model.DiscountPercentage,
            Price = model.Price,
            Rating = model.Rating,
            Stock = model.Stock,
            Thumbnail = model.Thumbnail,
            Title = model.Title,
            Images = string.Join(',', model.Images)
        };

    public static ProductModel ToDto(this Product model) =>
        new()
        {
            Id = model.Id,
            Brand = model.Brand,
            Category = model.Category,
            Description = model.Description,
            DiscountPercentage = model.DiscountPercentage,
            Price = model.Price,
            Rating = model.Rating,
            Stock = model.Stock,
            Thumbnail = model.Thumbnail,
            Title = model.Title,
            Images = model.Images.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
        };

    public static void UpdateWith(this Product modelToUpdate, ProductModel model)
    {
        modelToUpdate.Brand = model.Brand;
        modelToUpdate.Category = model.Category;
        modelToUpdate.Description = model.Description;
        modelToUpdate.DiscountPercentage = model.DiscountPercentage;
        modelToUpdate.Price = model.Price;
        modelToUpdate.Rating = model.Rating;
        modelToUpdate.Stock = model.Stock;
        modelToUpdate.Thumbnail = model.Thumbnail;
        modelToUpdate.Title = model.Title;
        modelToUpdate.Images = string.Join(',', model.Images);
    }

    
}
