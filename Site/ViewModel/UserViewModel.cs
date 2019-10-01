using System.ComponentModel.DataAnnotations;
using Site.Infra.Models;
using Site.ViewModel.Validation;

namespace Site.ViewModel
{
  public class UserViewModel
  {
    public UserViewModel() { }

    public UserViewModel(Usuario u)
    {
      Id = u.Id;
      Email = u.Email;
      Login = u.Login;
      Tipo = u.Tipo;
    }
    public int Id { get; set; }

    [RequiredField("Login")]
    [MinFieldLength("Login", 5)]
    public string Login { get; set; }

    [RequiredField("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [TypeUserField]
    public string Tipo { get; set; }
  }
}