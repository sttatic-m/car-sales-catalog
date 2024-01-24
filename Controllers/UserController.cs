using backend.Models;
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

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            var newUser = new User(Guid.NewGuid(), user.Name, user.Password);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(newUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}