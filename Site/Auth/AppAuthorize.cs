using Site.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Site.Auth
{
  public class AppAuthorize : AuthorizeAttribute
  {
    public AppAuthorize(AccessType type)
    {
      Roles = type == AccessType.Admin ? "ADMIN" : "USER";
    }
  }
}