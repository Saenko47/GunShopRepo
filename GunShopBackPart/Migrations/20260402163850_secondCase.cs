using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GunShopBackPart.Migrations
{
    /// <inheritdoc />
    public partial class secondCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProduct_Suppliers_SupplierId",
                table: "BaseProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_BaseProduct_ProductId",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseProduct",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "AmountInBox",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "Caliber",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "GunType",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "Gun_Caliber",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseProduct");

            migrationBuilder.RenameTable(
                name: "BaseProduct",
                newName: "BaseProducts");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProduct_SupplierId",
                table: "BaseProducts",
                newName: "IX_BaseProducts_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseProducts",
                table: "BaseProducts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessories_BaseProducts_Id",
                        column: x => x.Id,
                        principalTable: "BaseProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ammos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Caliber = table.Column<int>(type: "int", nullable: false),
                    AmountInBox = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ammos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ammos_BaseProducts_Id",
                        column: x => x.Id,
                        principalTable: "BaseProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caliber = table.Column<int>(type: "int", nullable: false),
                    GunType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guns_BaseProducts_Id",
                        column: x => x.Id,
                        principalTable: "BaseProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProducts_Suppliers_SupplierId",
                table: "BaseProducts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_BaseProducts_ProductId",
                table: "Storage",
                column: "ProductId",
                principalTable: "BaseProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProducts_Suppliers_SupplierId",
                table: "BaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_BaseProducts_ProductId",
                table: "Storage");

            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "Ammos");

            migrationBuilder.DropTable(
                name: "Guns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseProducts",
                table: "BaseProducts");

            migrationBuilder.RenameTable(
                name: "BaseProducts",
                newName: "BaseProduct");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProducts_SupplierId",
                table: "BaseProduct",
                newName: "IX_BaseProduct_SupplierId");

            migrationBuilder.AddColumn<int>(
                name: "AmountInBox",
                table: "BaseProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Caliber",
                table: "BaseProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseProduct",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GunType",
                table: "BaseProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gun_Caliber",
                table: "BaseProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseProduct",
                table: "BaseProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProduct_Suppliers_SupplierId",
                table: "BaseProduct",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_BaseProduct_ProductId",
                table: "Storage",
                column: "ProductId",
                principalTable: "BaseProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
