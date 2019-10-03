using Shop.Api.Helpers;
using Shop.Api.ViewModel;

namespace Shop.Api.Infra.Models
{
  public class User
  {
    public User() { }

    public User(CreateUserViewModel u)
    {
      Login = u.Login;
      Email = u.Email;
      Password = u.Password;
    }

    public int Id { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    public UserType Type { get; set; }

    public string Password { get; set; }
  }
}