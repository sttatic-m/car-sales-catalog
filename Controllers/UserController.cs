using backend.Models;
using BCrypt.Net;
using car_sales_catalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            string newPass = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var newUser = new User(Guid.NewGuid(), user.Name, newPass);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(newUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/login")]
    public async Task<IActionResult> UserLoggon([FromBody] User user)
    {
        try
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(ur => ur.Name == user.Name) ?? throw new Exception("Failed to Get this User Name");
            var result = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}