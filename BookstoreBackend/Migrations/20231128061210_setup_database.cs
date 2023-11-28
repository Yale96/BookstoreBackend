using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreBackend.Migrations
{
    /// <inheritdoc />
    public partial class setup_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "NumberOfPages", "Title" },
                values: new object[,]
                {
                    { 1, "Author 1", "Test Book 1", 101, "Test Book 1" },
                    { 2, "Author 2", "Test Book 2", 202, "Test Book 2" },
                    { 3, "Author 3", "Test Book 3", 303, "Test Book 3" },
                    { 4, "Author 4", "Test Book 4", 404, "Test Book 4" },
                    { 5, "Author 5", "Test Book 5", 505, "Test Book 5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
