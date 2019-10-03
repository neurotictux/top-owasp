using System;

namespace Shop.Api.Infra.Models
{
  public class Order
  {
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime Date { get; set; }

    public bool Delivered { get; set; }
  }
}