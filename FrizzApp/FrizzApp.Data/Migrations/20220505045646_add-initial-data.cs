using Microsoft.EntityFrameworkCore.Migrations;

namespace FrizzApp.Data.Migrations
{
    public partial class addinitialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "OrderStatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Done" },
                    { 3, "Canceled" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[,]
                {
                    { 10, "Cash" },
                    { 11, "MercadoPago" },
                    { 13, "Debit" },
                    { 14, "Credit" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypeId",
                keyValue: 14);
        }
    }
}
