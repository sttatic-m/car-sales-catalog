using Microsoft.AspNetCore.Mvc;

namespace car_sales_catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public ActionResult GetUser()
    {
        return Ok("Teste");
    }
}