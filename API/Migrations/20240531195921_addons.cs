using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c5c446-f51b-47d3-aa60-542317901552");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70e295ea-a9cf-4a35-bc8b-f5fe4259225d");

            migrationBuilder.AddColumn<bool>(
                name: "DescriptionType",
                table: "TransactionDescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TotalProfit",
                table: "FinancialGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f99618f-9c53-476f-ab85-7ea8d2f3baaa", null, "User", "USER" },
                    { "5ed3fbda-93ad-4a30-8cd4-bcb787b4bff3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f99618f-9c53-476f-ab85-7ea8d2f3baaa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed3fbda-93ad-4a30-8cd4-bcb787b4bff3");

            migrationBuilder.DropColumn(
                name: "DescriptionType",
                table: "TransactionDescriptions");

            migrationBuilder.DropColumn(
                name: "TotalProfit",
                table: "FinancialGoals");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33c5c446-f51b-47d3-aa60-542317901552", null, "Admin", "ADMIN" },
                    { "70e295ea-a9cf-4a35-bc8b-f5fe4259225d", null, "User", "USER" }
                });
        }
    }
}
