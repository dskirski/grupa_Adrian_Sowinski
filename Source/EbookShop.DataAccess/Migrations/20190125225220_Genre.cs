using Microsoft.EntityFrameworkCore.Migrations;

namespace EbookShop.DataAccess.Migrations
{
    public partial class Genre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EbookGenres_Categories_GenreId",
                table: "EbookGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Genres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_EbookGenres_Genres_GenreId",
                table: "EbookGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EbookGenres_Genres_GenreId",
                table: "EbookGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_EbookGenres_Categories_GenreId",
                table: "EbookGenres",
                column: "GenreId",
                principalTable: "Categories",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
