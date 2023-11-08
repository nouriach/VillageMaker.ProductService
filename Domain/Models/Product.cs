using System.ComponentModel.DataAnnotations;

namespace VillageMaker.ProductService.Domain.Models;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Category { get; set; }
    
    [Required]
    public int MakerId { get; set; }
    
    public Maker Maker { get; set; }
}