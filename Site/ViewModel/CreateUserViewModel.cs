using System.ComponentModel.DataAnnotations;
using Site.Infra.Models;
using Site.ViewModel.Validation;

namespace Site.ViewModel
{
  public class CreateUserViewModel
  {
    [RequiredField("Login")]
    [MinFieldLength("Login", 5)]
    public string Login { get; set; }

    [RequiredField("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [RequiredField("Password")]
    [MinFieldLength("Password", 6)]
    
    public string Password { get; set; }

    [TypeUserField]
    public string Tipo { get; set; }
  }
}