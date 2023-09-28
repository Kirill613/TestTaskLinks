using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShorteningApp.Migrations
{
    /// <inheritdoc />
    public partial class UrlInfoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UrlInfos");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UrlInfos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UrlInfos");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "UrlInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
