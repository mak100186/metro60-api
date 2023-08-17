using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Metro60.Core.Entities;

[DataContract]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string Description { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal Rating { get; set; }

    public int Stock { get; set; }

    [Required]
    public string Brand { get; set; }

    public string Category { get; set; }

    public string Thumbnail { get; set; }

    //public List<string> Images { get; set; }
}
