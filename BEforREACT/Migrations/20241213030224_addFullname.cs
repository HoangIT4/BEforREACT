﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEforREACT.Migrations
{
    /// <inheritdoc />
    public partial class addFullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Orders");
        }
    }
}
