using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEforREACT.Migrations
{
    /// <inheritdoc />
    public partial class CartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("607e8158-9a3f-4806-968f-83ec9774b7e2"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesBrands_BrandID",
                table: "CategoriesBrands",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesBrands_CategoryID",
                table: "CategoriesBrands",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesBrands_Brands_BrandID",
                table: "CategoriesBrands",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesBrands_Categories_CategoryID",
                table: "CategoriesBrands",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesBrands_Brands_BrandID",
                table: "CategoriesBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesBrands_Categories_CategoryID",
                table: "CategoriesBrands");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CategoriesBrands_BrandID",
                table: "CategoriesBrands");

            migrationBuilder.DropIndex(
                name: "IX_CategoriesBrands_CategoryID",
                table: "CategoriesBrands");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Carts");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "CreatedAt", "DeletedAt" },
                values: new object[] { new Guid("607e8158-9a3f-4806-968f-83ec9774b7e2"), "All Products", new DateTime(2024, 11, 29, 7, 50, 55, 58, DateTimeKind.Utc).AddTicks(763), null });
        }
    }
}
