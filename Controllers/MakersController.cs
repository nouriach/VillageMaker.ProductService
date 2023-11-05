using Microsoft.AspNetCore.Mvc;

namespace VillageMaker.ProductService.Controllers;

[Route("api/v1/p/[controller]")]
[ApiController]
public class MakersController : ControllerBase
{
    public MakersController()
    {
        
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("---> Inbound POST # Product Service");

        return Ok("---> Inbound Test Ok from Makers Controller");
    }
}