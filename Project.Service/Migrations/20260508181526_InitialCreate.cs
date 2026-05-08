using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Gadgets and devices", "Electronics" },
                    { 2, "Apparel and accessories", "Clothing" },
                    { 3, "Tools and decor", "Home & Garden" },
                    { 4, "Equipment and gear", "Sports" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "iPhone 15", 999.99m, 50 },
                    { 2, 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Samsung TV", 599.99m, 20 },
                    { 3, 1, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sony Headphones", 199.99m, 0 },
                    { 4, 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nike Shoes", 120.00m, 100 },
                    { 5, 2, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Levi's Jeans", 65.50m, 30 },
                    { 6, 2, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Winter Jacket", 150.00m, 10 },
                    { 7, 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Coffee Maker", 89.99m, 15 },
                    { 8, 3, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Garden Hose", 25.00m, 200 },
                    { 9, 4, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Yoga Mat", 30.00m, 45 },
                    { 10, 4, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Dumbbells", 45.99m, 12 },
                    { 11, 4, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Tennis Racket", 110.00m, 5 },
                    { 12, 4, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Running Socks", 15.00m, 500 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
