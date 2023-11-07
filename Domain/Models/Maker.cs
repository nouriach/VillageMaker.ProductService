using System.ComponentModel.DataAnnotations;

namespace VillageMaker.ProductService.Domain.Models;

public class Maker
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int ExternalId { get; set; }
    
    [Required]
    public string Postcode { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}