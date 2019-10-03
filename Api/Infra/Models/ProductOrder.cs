namespace Shop.Api.Infra.Models
{
  public class ProductOrder
  {
    public ProductOrder() { }

    public int Id { get; set; }

    public Product Product { get; set; }

    public Order Order { get; set; }
  }
}