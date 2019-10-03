using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Api.Infra.Models;

namespace Shop.Api.ViewModel
{
  public class OrderViewModel
  {
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string Status { get; set; }

    public DateTime Date { get; set; }

    public List<ProductViewModel> Products { get; set; }

    public decimal TotalCost
    {
      get
      {
        return Products.Select(p => p.Price * p.Count).Sum();
      }
    }

    public int TotalItems
    {
      get
      {
        return Products.Select(p => p.Count).Sum();
      }
    }

    public bool Delivered { get; set; }

    public OrderViewModel()
    {
      Products = new List<ProductViewModel>();
    }

    public static List<OrderViewModel> ToParse(List<ProductOrder> productOrders)
    {
      List<OrderViewModel> orders = new List<OrderViewModel>();
      foreach (var productOrder in productOrders.OrderBy(p => p.Order.Id).ThenBy(p => p.Product.Id))
      {
        if (!orders.Any(p => p.Id == productOrder.Order.Id))
          orders.Add(new OrderViewModel()
          {
            Id = productOrder.Order.Id,
            ClientId = productOrder.Order.ClientId,
            Date = productOrder.Order.Date,
            Delivered = productOrder.Order.Delivered
          });
        var orderViewModel = orders.First(p => p.Id == productOrder.Order.Id);
        var product = orderViewModel.Products.FirstOrDefault(p => p.Id == productOrder.Order.Id);

        if (product == null)
        {
          orderViewModel.Products.Add(new ProductViewModel()
          {
            Id = productOrder.Product.Id,
            Description = productOrder.Product.Description,
            Count = 1
          });
        }
        else
          product.Count++;
      }
      return orders;
    }
  }
}