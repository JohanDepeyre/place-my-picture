using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationPhoto.Web.UI.Data.Migrations
{
    public partial class Voyage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Categorie_CategorieId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_CategorieId",
                table: "Photo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePicture",
                table: "Photo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "VoyageId",
                table: "Photo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Voyage",
                columns: table => new
                {
                    VoyageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomVoyage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateVoyage = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyage", x => x.VoyageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voyage");

            migrationBuilder.DropColumn(
                name: "VoyageId",
                table: "Photo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePicture",
                table: "Photo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_CategorieId",
                table: "Photo",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Categorie_CategorieId",
                table: "Photo",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "CategorieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
