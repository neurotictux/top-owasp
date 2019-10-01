using System;
using System.Collections.Generic;
using System.Linq;
using Site.Infra.Models;

namespace Site.ViewModel
{
  public class PedidoViewModel
  {
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public string Situacao { get; set; }
    public DateTime Data { get; set; }
    public DateTime? DataExpedicao { get; set; }
    public DateTime? DataAceite { get; set; }
    public List<ProdutoViewModel> Produtos { get; set; }

    public decimal TotalPreco
    {
      get
      {
        return Produtos.Select(p => p.Preco * p.Count).Sum();
      }
    }

    public int TotalItens
    {
      get
      {
        return Produtos.Select(p => p.Count).Sum();
      }
    }

    public string DataAprovacao
    {
      get
      {
        return $"{DataAceite?.ToShortDateString()} - {DataAceite?.ToShortTimeString()}";
      }
    }

    public string DataFinalizacao
    {
      get
      {
        return $"{DataExpedicao?.ToShortDateString()} - {DataExpedicao?.ToShortTimeString()}";
      }
    }

    public PedidoViewModel()
    {
      Produtos = new List<ProdutoViewModel>();
    }

    public static List<PedidoViewModel> ToParse(List<VendaProduto> vendasProduto)
    {
      List<PedidoViewModel> vendas = new List<PedidoViewModel>();
      foreach (var vendaProduto in vendasProduto.OrderBy(p => p.Venda.Id).ThenBy(p => p.Produto.Id))
      {
        if (!vendas.Any(p => p.Id == vendaProduto.Venda.Id))
          vendas.Add(new PedidoViewModel()
          {
            Id = vendaProduto.Venda.Id,
            IdCliente = vendaProduto.Venda.IdCliente,
            Data = vendaProduto.Venda.Data,
            DataAceite = vendaProduto.Venda.DataAceite,
            DataExpedicao = vendaProduto.Venda.DataExpedicao,
            Situacao = vendaProduto.Venda.Situacao
          });
        var vendaViewModel = vendas.First(p => p.Id == vendaProduto.Venda.Id);
        var produto = vendaViewModel.Produtos.FirstOrDefault(p => p.Id == vendaProduto.Produto.Id);

        if (produto == null)
        {
          vendaViewModel.Produtos.Add(new ProdutoViewModel()
          {
            Id = vendaProduto.Produto.Id,
            Nome = vendaProduto.Produto.Nome,
            Preco = vendaProduto.Preco.Valor,
            DataValidade = vendaProduto.Produto.DataValidade,
            Count = 1
          });
        }
        else
          produto.Count++;
      }
      return vendas;
    }
  }
}