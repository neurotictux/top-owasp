using System.Linq;
using Shop.Api.Auth;
using Shop.Api.Helpers;
using Shop.Api.Infra;
using Shop.Api.Infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class CategoryController : BaseController
  {
    public CategoryController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get() => Ok(context.Category.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) => Ok(context.Category.FirstOrDefault(p => p.Id == id));

    [AppAuthorize(UserType.Admin)]
    [HttpPost]
    public void Post([FromBody] Category category)
    {
      if (string.IsNullOrWhiteSpace(category.Description))
        throw new ValidateException("A nova categoria precisa de um nome.");
      if (context.Category.FirstOrDefault(p => p.Description == category.Description) != null)
        throw new ValidateException("Já existe uma categoria com este nome.");
      context.Category.Add(new Category { Description = category.Description });
      context.SaveChanges();
    }

    [AppAuthorize(UserType.Admin)]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      if (context.Product.FirstOrDefault(p => p.Category.Id == id) != null)
        throw new ValidateException("Existem produtos utilizando esta categoria.");
      var c = context.Category.FirstOrDefault(p => p.Id == id);
      context.Category.Remove(c);
      context.SaveChanges();
    }
  }
}