using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEforREACT.Migrations
{
    /// <inheritdoc />
    public partial class OriginAndWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ProductDetails");
        }
    }
}
