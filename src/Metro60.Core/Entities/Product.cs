using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Metro60.Core.Entities;

//notes for reviewer: its best practice to duplicate the validations on both layers: api (requests) and the data layer.
//However, since there is no DBMS involved here, i cannot create Constraints and Checks to ensure that. So, i am NOT adding too many
//restrictions of this layer and they wont work anyway.
[DataContract]
public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //note: this doesn't work for the file based database. provide it manually
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [StringLength(100)]
    public string Description { get; set; }

    public double Price { get; set; }

    public double DiscountPercentage { get; set; }

    public double Rating { get; set; }

    public int Stock { get; set; }

    [Required]
    public string Brand { get; set; }

    public string Category { get; set; }

    public string Thumbnail { get; set; }

    public string Images { get; set; }
}
