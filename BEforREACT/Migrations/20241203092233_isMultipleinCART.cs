using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEforREACT.Migrations
{
    /// <inheritdoc />
    public partial class isMultipleinCART : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "OrderItems");

            migrationBuilder.AddColumn<bool>(
                name: "isMultiple",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMultiple",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
