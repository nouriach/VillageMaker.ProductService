using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VillageMaker.ProductService.Data.Interfaces;
using VillageMaker.ProductService.Domain.DTOs;
using VillageMaker.ProductService.Domain.Models;

namespace VillageMaker.ProductService.Controllers;

[Route("/api/v1/p/makers/{makerId}/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepo _repo;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductReadDto>> GetProductsForMaker(int makerId)
    {
        Console.WriteLine($"---> Hit GetProductsForMaker: {makerId}");
        
        if (!_repo.MakerExists(makerId))
        {
            return NotFound();
        }

        var productItems = _repo.GetProductsForMaker(makerId);
        return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
    }

    [HttpGet("{productId}", Name = "GetProductForMaker")]
    public ActionResult<ProductReadDto> GetProductForMaker(int makerId, int productId)
    {
        Console.WriteLine($"---> Hit GetProductForMaker; MakerId: {makerId}, ProductId: {productId}");
        
        if (!_repo.MakerExists(makerId))
        {
            return NotFound();
        }

        var productItem = _repo.GetProductForMaker(makerId, productId);
        
        if (productItem == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProductReadDto>(productItem));
    }

    [HttpPost]
    public ActionResult<ProductReadDto> CreateProductForMaker(int makerId, ProductCreateDto productDto)
    {
        Console.WriteLine($"---> Hit CreateProductForMaker. MakerId: {makerId}");

        if (!_repo.MakerExists(makerId))
        {
            return NotFound();
        }

        var product = _mapper.Map<Product>(productDto);
        
        _repo.CreateProduct(makerId, product);
        _repo.SaveChanges();

        var productReadDto = _mapper.Map<ProductReadDto>(product);

        return CreatedAtRoute(nameof(GetProductForMaker),
            new { makerId = makerId, productId = productReadDto.Id }, productReadDto);
    }
}