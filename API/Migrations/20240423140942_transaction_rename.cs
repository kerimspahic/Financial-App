using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class transaction_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeDescriptions");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a75dd581-298c-47f4-8b5b-88f107a19100");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd98094e-0fc0-420c-a581-cb1e01a9eb8a");

            migrationBuilder.CreateTable(
                name: "TransactionDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescriptionName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionAmount = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<bool>(type: "INTEGER", nullable: false),
                    TransactionDescription = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7e740d62-d821-4865-a8ee-45e45086ab6a", null, "User", "USER" },
                    { "a31e813e-c767-4833-abf8-6e46038d971a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AppUserId",
                table: "Transactions",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDescriptions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e740d62-d821-4865-a8ee-45e45086ab6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a31e813e-c767-4833-abf8-6e46038d971a");

            migrationBuilder.CreateTable(
                name: "ExchangeDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescriptionName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ExchangeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExchangeDate = table.Column<string>(type: "TEXT", nullable: true),
                    ExchangeDescription = table.Column<int>(type: "INTEGER", nullable: false),
                    ExchangeType = table.Column<bool>(type: "INTEGER", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchanges_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a75dd581-298c-47f4-8b5b-88f107a19100", null, "User", "USER" },
                    { "dd98094e-0fc0-420c-a581-cb1e01a9eb8a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_AppUserId",
                table: "Exchanges",
                column: "AppUserId");
        }
    }
}
