using Microsoft.EntityFrameworkCore.Migrations;

namespace FrizzApp.Data.Migrations
{
    public partial class ppeasdasdaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductStatus",
                keyColumn: "ProductStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductStatus",
                keyColumn: "ProductStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductStatus",
                keyColumn: "ProductStatusId",
                keyValue: 3);
        }
    }
}
