using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationPhoto.Web.UI.Data.Migrations
{
    public partial class AJoutdes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionVoyage",
                table: "Voyage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionVoyage",
                table: "Voyage");
        }
    }
}
