using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FrizzApp.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentTypeName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProductStates",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStates", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ProductStatus",
                columns: table => new
                {
                    ProductStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatus", x => x.ProductStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsDelivery = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: true),
                    ClientPhone = table.Column<string>(type: "TEXT", nullable: true),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ProductStates_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "ProductStates",
                        principalColumn: "OrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Presentation = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", precision: 7, scale: 2, nullable: false, defaultValue: 0m),
                    OldPrice = table.Column<decimal>(type: "TEXT", precision: 7, scale: 2, nullable: true),
                    IsPromo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ProductStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductStatus_ProductStatusId",
                        column: x => x.ProductStatusId,
                        principalTable: "ProductStatus",
                        principalColumn: "ProductStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 100, "Panaderia" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 101, "Salado" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 102, "Dulce" });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[] { 10, "Cash" });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[] { 11, "MercadoPago" });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[] { 13, "Debit" });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[] { 14, "Credit" });

            migrationBuilder.InsertData(
                table: "ProductStates",
                columns: new[] { "OrderStatusId", "StatusName" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "ProductStates",
                columns: new[] { "OrderStatusId", "StatusName" },
                values: new object[] { 2, "Done" });

            migrationBuilder.InsertData(
                table: "ProductStates",
                columns: new[] { "OrderStatusId", "StatusName" },
                values: new object[] { 3, "Canceled" });

            migrationBuilder.InsertData(
                table: "ProductStatus",
                columns: new[] { "ProductStatusId", "Name" },
                values: new object[] { 1, "Avaiable" });

            migrationBuilder.InsertData(
                table: "ProductStatus",
                columns: new[] { "ProductStatusId", "Name" },
                values: new object[] { 2, "WithoutStock" });

            migrationBuilder.InsertData(
                table: "ProductStatus",
                columns: new[] { "ProductStatusId", "Name" },
                values: new object[] { 3, "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentTypeId",
                table: "Orders",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductStatusId",
                table: "Products",
                column: "ProductStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "ProductStates");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductStatus");
        }
    }
}
