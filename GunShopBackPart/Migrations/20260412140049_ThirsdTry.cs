using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GunShopBackPart.Migrations
{
    /// <inheritdoc />
    public partial class ThirsdTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Guns");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accessories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BaseProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BaseProducts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseProducts");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Guns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
