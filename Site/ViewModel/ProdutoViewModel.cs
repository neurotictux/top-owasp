using System;
using System.Collections.Generic;
using System.Linq;
using Site.Infra.Models;
using Site.ViewModel.Validation;

namespace Site.ViewModel
{
  public class ProdutoViewModel
  {
    public int Id { get; set; }

    [RequiredField("Nome")]
    public string Nome { get; set; }

    public decimal Preco { get; set; }

    [RequiredField("DataValidade")]
    public DateTime DataValidade { get; set; }

    [RequiredField("Categoria")]
    public Categoria Categoria { get; set; }
    public int Count { get; set; }

    public List<Preco> Historico { get; set; }

    public ProdutoViewModel() { }
    public ProdutoViewModel(Produto p)
    {
      Id = p.Id;
      Nome = p.Nome;
      Preco = p.Preco.Valor;
      DataValidade = p.DataValidade;
      Categoria = p.Categoria;
      Historico = p.HistoricoPreco.OrderByDescending(x => x.Data).ToList();
    }

    public string PrecoMoney
    {
      get
      {
        return string.Format("{0:#.00}", Preco);
      }
    }
  }
}