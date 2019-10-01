using System;
using System.Collections.Generic;
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
  public class PedidoController : BaseController
  {
    public PedidoController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get()
    {
      IQueryable<VendaProduto> vendaProduto = context.VendaProduto
        .Include(p => p.Venda)
        .Include(p => p.Produto)
        .Include(p => p.Preco);

      if (!IsAdmin)
      {
        var userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value;
        vendaProduto = vendaProduto.Where(p => p.Venda.IdCliente == Convert.ToInt32(userId));
      }

      return Ok(PedidoViewModel.ToParse(vendaProduto.ToList()));
    }

    [AppAuthorize(AccessType.Admin)]
    [HttpPost("status/{id}")]
    public IActionResult ChangeStatus(int id)
    {
      var venda = context.Venda.FirstOrDefault(p => p.Id == id);
      if (venda == null)
        throw new ValidateException("Pedido não encontrado");

      switch (venda.Situacao)
      {
        case OrderStatus.IN_PROGRESS:
          venda.Situacao = OrderStatus.APPROVED;
          venda.DataAceite = DateTime.Now;
          break;
        case OrderStatus.APPROVED:
          venda.Situacao = OrderStatus.FINISHED;
          venda.DataExpedicao = DateTime.Now;
          break;
      }
      context.Entry<Venda>(venda).State = EntityState.Modified;
      context.SaveChanges();
      return Ok();
    }

    [AppAuthorize(AccessType.User)]
    [HttpPost]
    public IActionResult Post([FromBody]PedidoViewModel model)
    {
      var venda = new Venda();
      ValidateModel();
      if (model.Produtos == null || !model.Produtos.Any())
        throw new ValidateException("Insira itens no pedido.");

      var userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value;
      venda.IdCliente = Convert.ToInt32(userId);
      venda.Data = DateTime.Now;
      venda.Situacao = OrderStatus.IN_PROGRESS;

      List<VendaProduto> vendaProdutos = new List<VendaProduto>();
      foreach (ProdutoViewModel produto in model.Produtos)
      {
        Produto produtoAtualizado = context.Produto
          .Include(p => p.HistoricoPreco)
          .FirstOrDefault(p => p.Id == produto.Id);

        if (produtoAtualizado.Preco == null || produtoAtualizado.Preco.Valor != produto.Preco)
          throw new ValidateException($"O produto {produto.Nome} teve seu preço alterado.");

        int added = 0;
        while (produto.Count > added++)
          vendaProdutos.Add(new VendaProduto(venda, produtoAtualizado));
      }

      context.Venda.Add(venda);
      vendaProdutos.ForEach(p => context.VendaProduto.Add(p));
      context.SaveChanges();

      return Ok();
    }
  }
}