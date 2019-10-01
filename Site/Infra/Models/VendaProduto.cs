using System.Collections;

namespace Site.Infra.Models
{
  public class VendaProduto
  {
    public VendaProduto() { }

    public VendaProduto(Venda venda, Produto produto)
    {
      Venda = venda;
      Produto = produto;
      Preco = produto.Preco;
    }

    public int Id { get; set; }
    public Produto Produto { get; set; }
    public Venda Venda { get; set; }
    public Preco Preco { get; set; }
    public int Quantidade { get; set; }
  }
}