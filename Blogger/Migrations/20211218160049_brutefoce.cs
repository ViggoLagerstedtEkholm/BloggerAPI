using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogger.Migrations
{
    public partial class brutefoce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "Secrets");

            migrationBuilder.DropColumn(
                name: "Tries",
                table: "Secrets");

            migrationBuilder.CreateTable(
                name: "BruteForcePrevent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    Tries = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BruteForcePrevent", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BruteForcePrevent");

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "Secrets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tries",
                table: "Secrets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
