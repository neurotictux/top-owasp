using System.Linq;
using System.Security.Claims;
using Site.Auth;
using Site.Helpers;
using Site.Infra;
using Site.Infra.Models;
using Site.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class ProdutoController : BaseController
  {
    public ProdutoController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get()
    {
      var produtos = context.Produto.Include(p => p.Categoria).Include(p => p.HistoricoPreco);
      return Ok(produtos.Select(p => new ProdutoViewModel(p)));
    }

    [Route("{categoryId}/{order}")]
    public IActionResult Get(int categoryId, string order)
    {
      var produtos = context.Produto.Where(p => p.Categoria.Id == categoryId || categoryId == 0)
        .Include(p => p.Categoria).Include(p => p.HistoricoPreco).ToList().Select(p => new ProdutoViewModel(p));

      order = string.IsNullOrEmpty(order) ? "" : order;

      if (order.ToLower() == "asc")
        return Ok(produtos.OrderBy(p => p.Preco));
      else if (order.ToLower() == "desc")
        return Ok(produtos.OrderByDescending(p => p.Preco));
      else
        return Ok(produtos.OrderBy(p => p.Id));
    }

    [AppAuthorize(AccessType.Admin)]
    [HttpPost]
    public IActionResult Post([FromBody]ProdutoViewModel model)
    {
      ValidateModel();

      if (model.Preco <= 0)
        throw new ValidateException("O preço deve ser maior que zero");

      var p = new Produto(model);
      context.Entry<Produto>(p).State = EntityState.Added;
      context.Entry<Preco>(p.HistoricoPreco.First()).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpPut]
    [AppAuthorize(AccessType.Admin)]
    public IActionResult Put([FromBody]ProdutoViewModel model)
    {
      ValidateModel();

      if (model.Preco <= 0)
        throw new ValidateException("O preço deve ser maior que zero");

      bool updatePrice = model.Preco != model.Historico.OrderByDescending(x => x.Data).First().Valor;

      var p = new Produto(model);
      context.Entry<Produto>(p).State = EntityState.Modified;
      if (updatePrice)
        context.Entry<Preco>(p.HistoricoPreco.First()).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    [AppAuthorize(AccessType.Admin)]
    public IActionResult Delete(int id)
    {
      if (context.VendaProduto.FirstOrDefault(p => p.Produto.Id == id) != null)
        throw new ValidateException("O está vinculado à pedidos e não pode ser excluído.");
      var produto = context.Produto.Include(p => p.HistoricoPreco).FirstOrDefault(p => p.Id == id);
      context.Preco.RemoveRange(produto.HistoricoPreco);
      context.Produto.Remove(produto);
      context.SaveChanges();
      return Ok();
    }
  }
}