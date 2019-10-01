using Microsoft.EntityFrameworkCore;
using Site.Infra.Models;

namespace Site.Infra
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Preco> Preco { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Venda> Venda { get; set; }
    public DbSet<VendaProduto> VendaProduto { get; set; }
  }
}