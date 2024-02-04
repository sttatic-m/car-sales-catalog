using System.Globalization;
using car_sales_catalog.Data;
using car_sales_catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_sales_catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController (AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet(Name = "GetAll")] public IActionResult GetAll()
    {
        try
        {
            return Ok(_context.Cars.OrderBy(c => c.Code).ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost(Name = "AddCar")] public async Task<IActionResult> AddCar([FromBody] Car body)
    {
        try
        {
            int code = 0;
            var lastCar = _context.Cars.OrderByDescending(c => c.Code).FirstOrDefault();
            code = lastCar != null ? lastCar.Code : 0;

            var newCar = new Car(Guid.NewGuid(), ++code, body.Name, body.Price, body.releaseDate, body.ImageDir) ?? throw new Exception("Verify body");
            await _context.Cars.AddAsync(newCar);
            await _context.SaveChangesAsync();

            return Ok(newCar);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("/index", Name = "EditCar")] public async Task<IActionResult> EditCar(int index, [FromBody]Car body)
    {
        try
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Code == index) ?? throw new KeyNotFoundException("Not FOUND");

            car.Name = body.Name;
            car.Price = body.Price;
            car.releaseDate = body.releaseDate;
            car.ImageDir = body.ImageDir;

            _context.Update(car);
            await _context.SaveChangesAsync();

            return Ok("Car edited sucessfuly");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("/index", Name = "DeleteCar")] public async Task<IActionResult> DeleteCar(int index)
    {
        try
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Code == index) ?? throw new KeyNotFoundException("Not found");
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok("Car deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}