namespace VillageMaker.ProductService.Domain.DTOs;

public class ProductReadDto
{
    public int Id { get; set; }
    public string Category { get; set; }
    public int MakerId { get; set; }
}