using System;
using System.ComponentModel.DataAnnotations;

namespace Site.ViewModel.Validation
{
  public class TypeUserField : RequiredAttribute
  {
    public override bool IsValid(object value)
    {
      if (value == null)
      {
        ErrorMessage = "O campo 'Tipo' é requerido";
        return false;
      }


      var type = value.ToString().ToUpper();
      if (type != "ADMIN" && type != "USER")
      {
        ErrorMessage = "O tipo de usuário deve ser 'ADMIN' ou 'USER'.";
        return false;
      }

      return true;
    }
  }
}