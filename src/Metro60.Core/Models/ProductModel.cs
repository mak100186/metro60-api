using System.ComponentModel.DataAnnotations;

namespace Metro60.Core.Models;

public class ProductModel
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public decimal DiscountPercentage { get; set; }

    [Range(1, 5)]
    public decimal Rating { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Stock { get; set; }

    [Required]
    public string Brand { get; set; }

    public string Category { get; set; }

    public string Thumbnail { get; set; }

    public List<string> Images { get; set; }
}
