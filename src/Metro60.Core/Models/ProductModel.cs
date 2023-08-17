using System.ComponentModel.DataAnnotations;

namespace Metro60.Core.Models;

public class ProductModel
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    [StringLength(100)]
    public string Description { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Price should be greater than 0")]
    public double Price { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Discount percentage should be greater than or equal to 0")]
    public double DiscountPercentage { get; set; } = 0;

    [Range(0, 5)]
    public double Rating { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Stock { get; set; } = 0;

    [Required]
    public string Brand { get; set; }

    public string Category { get; set; }

    public string Thumbnail { get; set; }

    public List<string> Images { get; set; } = new();
}
