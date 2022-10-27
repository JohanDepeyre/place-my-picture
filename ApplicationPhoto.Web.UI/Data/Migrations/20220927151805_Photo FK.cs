using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationPhoto.Web.UI.Data.Migrations
{
    public partial class PhotoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Photo_CategorieId",
                table: "Photo",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_VoyageId",
                table: "Photo",
                column: "VoyageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Categorie_CategorieId",
                table: "Photo",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "CategorieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Voyage_VoyageId",
                table: "Photo",
                column: "VoyageId",
                principalTable: "Voyage",
                principalColumn: "VoyageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Categorie_CategorieId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Voyage_VoyageId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_CategorieId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_VoyageId",
                table: "Photo");
        }
    }
}
