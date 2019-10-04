using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Shop.Api.Infra.Models;

namespace Shop.Api.Helpers
{
  public class PreDefinedData
  {
    public List<User> Users { get; set; }
    public List<Category> Categories { get; set; }
    public List<Product> Products { get; set; }

    public static PreDefinedData LoadData()
    {
      string json = File.ReadAllText("./predefineddata.json");
      return JsonConvert.DeserializeObject<PreDefinedData>(json);
    }
  }
}