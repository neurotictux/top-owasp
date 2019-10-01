using Site.ViewModel;

namespace Site.Infra.Models
{
  public class Usuario
  {
    public Usuario() { }
    public Usuario(CreateUserViewModel u)
    {
      Login = u.Login;
      Email = u.Email;
      Tipo = u.Tipo;
      Password = u.Password;
    }
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Tipo { get; set; }
    public string Password { get; set; }
  }
}