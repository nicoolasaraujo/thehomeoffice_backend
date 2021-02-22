using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Migrations
{
    public partial class createDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    UserAddress = table.Column<Address>(type: "jsonb", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "Password", "UserAddress" },
                values: new object[] { -1, "admin@admin.com", true, "admin", "admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsAdmin",
                table: "Users",
                column: "IsAdmin",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
