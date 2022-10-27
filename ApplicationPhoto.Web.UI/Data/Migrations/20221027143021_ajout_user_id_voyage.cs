using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationPhoto.Web.UI.Data.Migrations
{
    public partial class ajout_user_id_voyage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Voyage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Voyage");
        }
    }
}
