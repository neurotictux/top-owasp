using Shop.Api.Infra.Models;

namespace Shop.Api.ViewModel
{
  public class ProductViewModel
  {
    public int Id { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public Category Category { get; set; }

    public int Count { get; set; }

    public ProductViewModel() { }

    public ProductViewModel(Product p)
    {
      Id = p.Id;
      Description = p.Description;
      Price = p.Price;
      Category = p.Category;
    }

    public string PriceMoney
    {
      get
      {
        return string.Format("{0:#.00}", Price);
      }
    }
  }
}