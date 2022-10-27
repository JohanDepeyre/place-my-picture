using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationPhoto.Web.UI.Data.Migrations
{
    public partial class VoyageDateFin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateVoyage",
                table: "Voyage",
                newName: "DateVoyageFin");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateVoyageDebut",
                table: "Voyage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateVoyageDebut",
                table: "Voyage");

            migrationBuilder.RenameColumn(
                name: "DateVoyageFin",
                table: "Voyage",
                newName: "DateVoyage");
        }
    }
}
