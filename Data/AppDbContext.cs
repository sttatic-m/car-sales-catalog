using backend.Models;
using car_sales_catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace car_sales_catalog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    
}