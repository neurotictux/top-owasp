using Shop.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Shop.Api.Auth
{
  public class AppAuthorize : AuthorizeAttribute
  {
    public AppAuthorize(params UserType[] types) => Roles = string.Join(",", types);
  }
}