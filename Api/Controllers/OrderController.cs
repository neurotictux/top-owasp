using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Shop.Api.Auth;
using Shop.Api.Helpers;
using Shop.Api.Infra;
using Shop.Api.Infra.Models;
using Shop.Api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class OrderController : BaseController
  {
    public OrderController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get()
    {
      IQueryable<ProductOrder> productOrder = context.ProductOrder
        .Include(p => p.Order)
        .Include(p => p.Product);

      if (!IsAdmin)
      {
        var userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value;
        productOrder = productOrder.Where(p => p.Order.ClientId == Convert.ToInt32(userId));
      }

      return Ok(OrderViewModel.ToParse(productOrder.ToList()));
    }

    [AppAuthorize(UserType.Salesman)]
    [HttpPost("status/{id}")]
    public IActionResult ChangeStatus(int id)
    {
      var order = context.Order.FirstOrDefault(p => p.Id == id);
      if (order == null)
        throw new ValidateException("Order not found");

      order.Delivered = !order.Delivered;

      context.Entry<Order>(order).State = EntityState.Modified;
      context.SaveChanges();
      return Ok();
    }

    [AppAuthorize(UserType.Client)]
    [HttpPost]
    public IActionResult Post([FromBody]OrderViewModel model)
    {
      var order = new Order();
      ValidateModel();
      if (model.Products == null || !model.Products.Any())
        throw new ValidateException("Insira itens no pedido.");

      var userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value;
      order.ClientId = Convert.ToInt32(userId);

      List<ProductOrder> productOrders = new List<ProductOrder>();
      foreach (var product in model.Products)
      {
        Product updatedProduct = context.Product.FirstOrDefault(p => p.Id == product.Id);

        int added = 0;
        while (product.Count > added++)
          productOrders.Add(new ProductOrder()
          {
            Product = updatedProduct,
            Order = order
          });
      }

      context.Order.Add(order);
      productOrders.ForEach(p => context.ProductOrder.Add(p));
      context.SaveChanges();

      return Ok();
    }
  }
}