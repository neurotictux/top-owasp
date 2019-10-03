using System.Linq;
using Shop.Api.Auth;
using Shop.Api.Helpers;
using Shop.Api.Infra;
using Shop.Api.Infra.Models;
using Shop.Api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class ProductController : BaseController
  {
    public ProductController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get()
    {
      var products = context.Product.Include(p => p.Category);
      return Ok(products.Select(p => new ProductViewModel(p)));
    }

    [Route("{categoryId}")]
    public IActionResult Get(int categoryId)
    {
      var products = context.Product.Where(p => p.Category.Id == categoryId || categoryId == 0)
        .Include(p => p.Category).Select(p => new ProductViewModel(p));
      return Ok(products.OrderBy(p => p.Id));
    }

    [AppAuthorize(UserType.Salesman, UserType.Admin)]
    [HttpPost]
    public IActionResult Post([FromBody]ProductViewModel model)
    {
      ValidateModel();

      if (model.Price <= 0)
        throw new ValidateException("The price must be greater than zero.");

      var p = new Product(model);
      context.Entry<Product>(p).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpPut]
    [AppAuthorize(UserType.Admin)]
    public IActionResult Put([FromBody]ProductViewModel model)
    {
      ValidateModel();

      if (model.Price <= 0)
        throw new ValidateException("The price must be greater than zero.");

      var product = new Product(model);
      context.Entry<Product>(product).State = EntityState.Modified;
      context.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    [AppAuthorize(UserType.Admin)]
    public IActionResult Delete(int id)
    {
      if (context.ProductOrder.Any(p => p.Product.Id == id))
        throw new ValidateException("This product is order bound and cannot be removed.");
      var product = context.Product.FirstOrDefault(p => p.Id == id);
      context.Product.Remove(product);
      context.SaveChanges();
      return Ok();
    }
  }
}