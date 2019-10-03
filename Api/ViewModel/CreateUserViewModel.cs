using System.ComponentModel.DataAnnotations;
using Shop.Api.Infra.Models;
using Shop.Api.ViewModel.Validation;

namespace Shop.Api.ViewModel
{
  public class CreateUserViewModel
  {
    [RequiredField("Login")]
    [MinFieldLength("Login", 5)]
    public string Login { get; set; }

    [RequiredField("Email")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; }

    [RequiredField("Password")]
    [MinFieldLength("Password", 6)]
    
    public string Password { get; set; }

    [TypeUserField]
    public string Tipo { get; set; }
  }
}