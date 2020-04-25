using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restful_API.Migrations
{
    public partial class v02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeatherForecast_User_UserId",
                table: "UserWeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWeatherForecast",
                table: "UserWeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserWeatherForecast");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "UserLogin",
                table: "UserWeatherForecast",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWeatherForecast",
                table: "UserWeatherForecast",
                columns: new[] { "UserLogin", "WeatherForecastId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Login");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeatherForecast_User_UserLogin",
                table: "UserWeatherForecast",
                column: "UserLogin",
                principalTable: "User",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeatherForecast_User_UserLogin",
                table: "UserWeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWeatherForecast",
                table: "UserWeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserLogin",
                table: "UserWeatherForecast");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserWeatherForecast",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWeatherForecast",
                table: "UserWeatherForecast",
                columns: new[] { "UserId", "WeatherForecastId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeatherForecast_User_UserId",
                table: "UserWeatherForecast",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
