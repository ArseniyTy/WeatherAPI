using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restful_API.Migrations
{
    public partial class v04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JWTs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Datetime = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    UserLogin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWTs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JWTs_Users_UserLogin",
                        column: x => x.UserLogin,
                        principalTable: "Users",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JWTs_UserLogin",
                table: "JWTs",
                column: "UserLogin",
                unique: true,
                filter: "[UserLogin] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JWTs");
        }
    }
}
