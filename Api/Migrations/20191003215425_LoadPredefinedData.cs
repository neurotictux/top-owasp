using Microsoft.EntityFrameworkCore.Migrations;
using Shop.Api.Helpers;

namespace Api.Migrations
{
  public partial class LoadPredefinedData : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      var data = PreDefinedData.LoadData();
      data.Users.ForEach(u => migrationBuilder.Sql($"INSERT INTO User (Email, Login, Password, Type) VALUES ('{u.Email}', '{u.Login}', '{u.Password}', {(int)u.Type})"));

      data.Categories.ForEach(c => migrationBuilder.Sql($"INSERT INTO Category (Description) VALUES ('{c.Description}')"));

      data.Products.ForEach(p => migrationBuilder.Sql($"INSERT INTO Product (Description, Price, CategoryId) VALUES ('{p.Description}', {p.Price}, {p.CategoryId})"));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
  }
}
