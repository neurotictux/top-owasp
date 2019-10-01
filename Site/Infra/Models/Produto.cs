using System;
using System.Collections.Generic;
using System.Linq;
using Site.ViewModel;

namespace Site.Infra.Models
{
  public class Produto
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Preco> HistoricoPreco { get; set; }
    public DateTime DataValidade { get; set; }
    public Categoria Categoria { get; set; }
    public int CategoriaId { get; set; }

    public Preco Preco
    {
      get
      {
        return HistoricoPreco.OrderByDescending(p => p.Data).FirstOrDefault() ?? new Preco() { };
      }
    }

    public Produto() { }
    public Produto(ProdutoViewModel model)
    {
      Id = model.Id;
      Nome = model.Nome;
      HistoricoPreco = new List<Preco>()
      {
        new Preco() { Data = DateTime.Now, Valor = model.Preco }
      };
      DataValidade = model.DataValidade;
      Categoria = model.Categoria;
      CategoriaId = model.Categoria.Id;
    }
  }
}