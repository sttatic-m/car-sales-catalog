using car_sales_catalog.Data;
using Microsoft.AspNetCore.Mvc;

namespace car_sales_catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpGet]
    public IActionResult GetUser()
    {
        try
        {
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}