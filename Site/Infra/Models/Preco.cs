using System;

namespace Site.Infra.Models
{
  public class Preco
  {
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }

    public string ValorMoney
    {
      get
      {
        return string.Format("{0:#.00}", Valor);
      }
    }

    public string ShortDateTime
    {
      get
      {
        return $"{Data.ToShortDateString()} - {Data.ToShortTimeString()}";
      }
    }
  }
}