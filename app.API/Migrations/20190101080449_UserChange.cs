using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace app.API.Migrations
{
    public partial class UserChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHashX",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSaltX",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHashX",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSaltX",
                table: "AspNetUsers");
        }
    }
}
