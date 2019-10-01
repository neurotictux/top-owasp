using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Site.Auth
{
  public sealed class JwtToken
  {
    private JwtSecurityToken token;
    internal JwtToken(JwtSecurityToken token)
    {
      this.token = token;
    }
    public DateTime ValidTo => token.ValidTo;
    public string Value => new JwtSecurityTokenHandler().WriteToken(token);
  }
}