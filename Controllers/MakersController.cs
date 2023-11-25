using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VillageMaker.ProductService.Data.Interfaces;
using VillageMaker.ProductService.Domain.DTOs;

namespace VillageMaker.ProductService.Controllers;

[Route("api/v1/p/[controller]")]
[ApiController]
public class MakersController : ControllerBase
{
    private readonly IProductRepo _repo;
    private readonly IMapper _mapper;

    public MakersController(IProductRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MakerReadDto>> GetMakers()
    {
        Console.WriteLine("---> Getting Makers from ProductService");
        
        var makerItems = _repo.GetAllMakers();

        return Ok(_mapper.Map<IEnumerable<MakerReadDto>>(makerItems));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("---> Inbound POST # Product Service");

        return Ok("---> Inbound Test Ok from Makers Controller");
    }
}