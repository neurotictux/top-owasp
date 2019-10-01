using System;

namespace Site.Infra.Models
{
  public class Venda
  {
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public DateTime Data { get; set; }
    public string Situacao { get; set; }
    public DateTime? DataExpedicao { get; set; }
    public DateTime? DataAceite { get; set; }
  }
}