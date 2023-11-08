using AutoMapper;
using VillageMaker.ProductService.Domain.DTOs;
using VillageMaker.ProductService.Domain.Models;

namespace VillageMaker.ProductService.Applications.Profiles;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        // Source -> Target
        // What the request sends, and then what the database returns
        CreateMap<Maker, MakerReadDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<Product, ProductReadDto>();
    }
}