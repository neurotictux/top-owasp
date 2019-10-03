using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Shop.Api.Helpers
{
  public class ExceptionHandling
  {
    private readonly RequestDelegate next;

    public ExceptionHandling(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      string result = null;
      int statusCode = 500;

      if (exception is ValidateException)
      {
        result = JsonConvert.SerializeObject(new { errors = ((ValidateException)exception).Errors });
        statusCode = 200;
      }
      else
        result = JsonConvert.SerializeObject(new { error = exception.Message });

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = statusCode;
      return context.Response.WriteAsync(result);
    }
  }
}