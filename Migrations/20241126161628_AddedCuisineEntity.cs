using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReserveBite.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedCuisineEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "CuisineId",
                table: "Restaurants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Restaurants",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cuisines",
                columns: new[] { "Id", "ImgUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://amazingfoodanddrink.com/wp-content/uploads/2024/05/The-Flavors-of-Italian-Street-Food_-259434423.jpg", "Italian" },
                    { 2, "https://kavala-online.com/wp-content/uploads/2024/08/greek-food-plate-1024x585.webp", "Greek" },
                    { 3, "https://tripjive.com/wp-content/uploads/2024/06/Where-to-eat-traditional-Bulgarian-food-in-Sofia.jpg", "Bulgarian" },
                    { 4, "https://www.tastingtable.com/img/gallery/20-delicious-indian-dishes-you-have-to-try-at-least-once/intro-1645057933.jpg", "Indian" },
                    { 5, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7PrURqk9v5JSOVaUKkSvFgNsqePcWfebTnQ&s", "Mexican" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CuisineId",
                table: "Restaurants",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Cuisines_CuisineId",
                table: "Restaurants",
                column: "CuisineId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Cuisines_CuisineId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CuisineId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
