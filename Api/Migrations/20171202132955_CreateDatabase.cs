using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Shop.Api.Migrations
{
  public partial class CreateDatabase : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Categoria",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Nome = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categoria", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Usuario",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Email = table.Column<string>(type: "TEXT", nullable: true),
            Login = table.Column<string>(type: "TEXT", nullable: true),
            Password = table.Column<string>(type: "TEXT", nullable: true),
            Tipo = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Usuario", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Venda",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Data = table.Column<DateTime>(type: "TEXT", nullable: false),
            DataAceite = table.Column<DateTime>(type: "TEXT", nullable: true),
            DataExpedicao = table.Column<DateTime>(type: "TEXT", nullable: true),
            IdCliente = table.Column<int>(type: "INTEGER", nullable: false),
            Situacao = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Venda", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Produto",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            CategoriaId = table.Column<int>(type: "INTEGER", nullable: true),
            DataValidade = table.Column<DateTime>(type: "TEXT", nullable: false),
            Nome = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Produto", x => x.Id);
            table.ForeignKey(
                      name: "FK_Produto_Categoria_CategoriaId",
                      column: x => x.CategoriaId,
                      principalTable: "Categoria",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateTable(
          name: "Preco",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Data = table.Column<DateTime>(type: "TEXT", nullable: false),
            ProdutoId = table.Column<int>(type: "INTEGER", nullable: true),
            Valor = table.Column<decimal>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Preco", x => x.Id);
            table.ForeignKey(
                      name: "FK_Preco_Produto_ProdutoId",
                      column: x => x.ProdutoId,
                      principalTable: "Produto",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateTable(
          name: "VendaProduto",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            PrecoId = table.Column<int>(type: "INTEGER", nullable: true),
            ProdutoId = table.Column<int>(type: "INTEGER", nullable: true),
            Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
            VendaId = table.Column<int>(type: "INTEGER", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_VendaProduto", x => x.Id);
            table.ForeignKey(
                      name: "FK_VendaProduto_Preco_PrecoId",
                      column: x => x.PrecoId,
                      principalTable: "Preco",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_VendaProduto_Produto_ProdutoId",
                      column: x => x.ProdutoId,
                      principalTable: "Produto",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_VendaProduto_Venda_VendaId",
                      column: x => x.VendaId,
                      principalTable: "Venda",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Preco_ProdutoId",
          table: "Preco",
          column: "ProdutoId");

      migrationBuilder.CreateIndex(
          name: "IX_Produto_CategoriaId",
          table: "Produto",
          column: "CategoriaId");

      migrationBuilder.CreateIndex(
          name: "IX_VendaProduto_PrecoId",
          table: "VendaProduto",
          column: "PrecoId");

      migrationBuilder.CreateIndex(
          name: "IX_VendaProduto_ProdutoId",
          table: "VendaProduto",
          column: "ProdutoId");

      migrationBuilder.CreateIndex(
          name: "IX_VendaProduto_VendaId",
          table: "VendaProduto",
          column: "VendaId");

      migrationBuilder.Sql("INSERT INTO Usuario (Email, Login, Password, Tipo) VALUES ('admin@mail.com', 'admin', '123qwe', 'Admin')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Usuario");

      migrationBuilder.DropTable(
          name: "VendaProduto");

      migrationBuilder.DropTable(
          name: "Preco");

      migrationBuilder.DropTable(
          name: "Venda");

      migrationBuilder.DropTable(
          name: "Produto");

      migrationBuilder.DropTable(
          name: "Categoria");
    }
  }
}
