using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEforREACT.Migrations
{
    /// <inheritdoc />
    public partial class CateBrandtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("91899901-7416-4559-961d-cd029b08b018"));

            migrationBuilder.CreateTable(
                name: "CategoriesBrands",
                columns: table => new
                {
                    CategoryBrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesBrands", x => x.CategoryBrandID);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "CreatedAt", "DeletedAt" },
                values: new object[] { new Guid("d1121cbd-d464-4843-870c-e7de3c0cbc85"), "All Products", new DateTime(2024, 11, 28, 2, 35, 33, 66, DateTimeKind.Utc).AddTicks(329), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesBrands");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d1121cbd-d464-4843-870c-e7de3c0cbc85"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "CreatedAt", "DeletedAt" },
                values: new object[] { new Guid("91899901-7416-4559-961d-cd029b08b018"), "All Products", new DateTime(2024, 11, 27, 17, 34, 28, 441, DateTimeKind.Utc).AddTicks(2336), null });
        }
    }
}
