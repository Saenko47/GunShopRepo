using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GunShopBackPart.Migrations
{
    /// <inheritdoc />
    public partial class eedroducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BaseProducts",
                columns: new[] { "Id", "Description", "Name", "Price", "SupplierId" },
                values: new object[,]
                {
                    { 1, "9mm pistol", "Glock 17", 500m, 1 },
                    { 2, ".45 ACP pistol", "Colt 1911", 700m, 2 },
                    { 3, "5.56 rifle", "AR-15", 1200m, 3 },
                    { 4, "7.62 rifle", "AK-47", 1100m, 4 },
                    { 5, "12 gauge shotgun", "Remington 870", 400m, 5 },
                    { 6, "50 rounds", "9mm Ammo Box", 20m, 1 },
                    { 7, "50 rounds", ".45 ACP Ammo", 25m, 2 },
                    { 8, "30 rounds", "5.56 Ammo", 18m, 3 },
                    { 9, "30 rounds", "7.62 Ammo", 22m, 4 },
                    { 10, "25 shells", "12 Gauge Shells", 15m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Ammos",
                columns: new[] { "Id", "AmountInBox", "Caliber" },
                values: new object[,]
                {
                    { 6, 50, 0 },
                    { 7, 50, 1 },
                    { 8, 30, 2 },
                    { 9, 30, 3 },
                    { 10, 25, 4 }
                });

            migrationBuilder.InsertData(
                table: "Guns",
                columns: new[] { "Id", "Caliber", "GunType" },
                values: new object[,]
                {
                    { 1, 0, 0 },
                    { 2, 1, 0 },
                    { 3, 2, 1 },
                    { 4, 3, 1 },
                    { 5, 4, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ammos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ammos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ammos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ammos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ammos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Guns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guns",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Guns",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Guns",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Guns",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BaseProducts",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
