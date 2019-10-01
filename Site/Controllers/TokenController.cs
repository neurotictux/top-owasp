using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Site.Auth;
using Site.Infra;
using Site.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
  [Route("token")]
  public class TokenController : Controller
  {
    private AppDbContext context;

    public TokenController(AppDbContext context) { this.context = context; }

    [HttpPost]
    public IActionResult Post([FromBody]LoginViewModel model)
    {
      if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
        return Unauthorized();

      var user = context.Usuario.FirstOrDefault(p => p.Login == model.Login);
      if (user == null || user.Password != model.Password)
        return Unauthorized();

      var claims = new Dictionary<string, string>();
      claims.Add(ClaimTypes.Sid, user.Id.ToString());
      claims.Add(ClaimTypes.Role, user.Tipo.ToUpper());
      var token = new JwtTokenBuilder(claims).Build();
      return Ok(new { token = token.Value, user = new UserViewModel(user) });
    }
  }
}