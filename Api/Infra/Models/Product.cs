using Shop.Api.ViewModel;

namespace Shop.Api.Infra.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public Product() { }

    public Product(ProductViewModel model)
    {
      Id = model.Id;
      Description = model.Description;
      Price = model.Price;
      Category = model.Category;
      CategoryId = model.Category.Id;
    }
  }
}