using System.ComponentModel.DataAnnotations;
using Shop.Api.Helpers;
using Shop.Api.Infra.Models;
using Shop.Api.ViewModel.Validation;

namespace Shop.Api.ViewModel
{
  public class UserViewModel
  {
    public UserViewModel() { }

    public UserViewModel(User u)
    {
      Id = u.Id;
      Email = u.Email;
      Login = u.Login;
      Type = u.Type;
    }
    public int Id { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    public UserType Type { get; set; }
  }
}