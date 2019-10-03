using Microsoft.EntityFrameworkCore;
using Shop.Api.Infra.Models;

namespace Shop.Api.Infra
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<ProductOrder> ProductOrder { get; set; }
  }
}