using Microsoft.EntityFrameworkCore.Migrations;

namespace restful_API.Migrations
{
    public partial class v03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeatherForecast_User_UserLogin",
                table: "UserWeatherForecast");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWeatherForecast_WeatherForecasts_WeatherForecastId",
                table: "UserWeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWeatherForecast",
                table: "UserWeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "UserWeatherForecast",
                newName: "UserWeatherForecasts");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeatherForecast_WeatherForecastId",
                table: "UserWeatherForecasts",
                newName: "IX_UserWeatherForecasts_WeatherForecastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWeatherForecasts",
                table: "UserWeatherForecasts",
                columns: new[] { "UserLogin", "WeatherForecastId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Login");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeatherForecasts_Users_UserLogin",
                table: "UserWeatherForecasts",
                column: "UserLogin",
                principalTable: "Users",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeatherForecasts_WeatherForecasts_WeatherForecastId",
                table: "UserWeatherForecasts",
                column: "WeatherForecastId",
                principalTable: "WeatherForecasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeatherForecasts_Users_UserLogin",
                table: "UserWeatherForecasts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWeatherForecasts_WeatherForecasts_WeatherForecastId",
                table: "UserWeatherForecasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWeatherForecasts",
                table: "UserWeatherForecasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserWeatherForecasts",
                newName: "UserWeatherForecast");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeatherForecasts_WeatherForecastId",
                table: "UserWeatherForecast",
                newName: "IX_UserWeatherForecast_WeatherForecastId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeatherForecast_WeatherForecasts_WeatherForecastId",
                table: "UserWeatherForecast",
                column: "WeatherForecastId",
                principalTable: "WeatherForecasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
