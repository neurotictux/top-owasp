using System.ComponentModel.DataAnnotations;

namespace Site.ViewModel.Validation
{
  public class MinFieldLength : MinLengthAttribute
  {
    public MinFieldLength(string fieldName, int length) : base(length)
    {
      ErrorMessage = $"O campo '{fieldName} deve ter no mínimo '{length}' caracteres";
    }
  }
}