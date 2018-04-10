using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace g4u.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('PC')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('PS3')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('PS4')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('XBOX 360')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('XBOX ONE')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('NINTENDO')");

            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('MMORPG')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('FPS')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('TPS')");
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Products_ProductId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_ProductId",
                table: "Photos",
                newName: "IX_Photos_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Name IN ('PC','PS3','PS4','XBOX 360','XBOX ONE', 'NINTENDO')");
            migrationBuilder.Sql("DELETE FROM Categories WHERE Name In ('MMORPG', 'FPS', 'TPS'");
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_ProductId",
                table: "Photo",
                newName: "IX_Photo_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Products_ProductId",
                table: "Photo",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
