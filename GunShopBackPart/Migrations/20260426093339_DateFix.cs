using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GunShopBackPart.Migrations
{
    /// <inheritdoc />
    public partial class DateFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GunPurchases_Customers_CustomerId",
                table: "GunPurchases");

            migrationBuilder.DropIndex(
                name: "IX_GunPurchases_InventoryItemId",
                table: "GunPurchases");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Storage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Storage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldAt",
                table: "Storage",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Licenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedAt",
                table: "Licenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseAt",
                table: "GunPurchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BaseProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storage_Id",
                table: "Storage",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storage_SerialNumber",
                table: "Storage",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GunPurchases_InventoryItemId",
                table: "GunPurchases",
                column: "InventoryItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GunPurchases_Customers_CustomerId",
                table: "GunPurchases",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GunPurchases_Customers_CustomerId",
                table: "GunPurchases");

            migrationBuilder.DropIndex(
                name: "IX_Storage_Id",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_Storage_SerialNumber",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_GunPurchases_InventoryItemId",
                table: "GunPurchases");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Storage");

            migrationBuilder.DropColumn(
                name: "SoldAt",
                table: "Storage");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "IssuedAt",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "PurchaseAt",
                table: "GunPurchases");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Storage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BaseProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GunPurchases_InventoryItemId",
                table: "GunPurchases",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GunPurchases_Customers_CustomerId",
                table: "GunPurchases",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
