using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace car_sales_catalog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    DbSet<User> Users { get; set; }
}