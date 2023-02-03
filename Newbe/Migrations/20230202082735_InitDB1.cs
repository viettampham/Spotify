using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Newbe.Migrations
{
    public partial class InitDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LovedSongs",
                table: "LovedSongs");

            migrationBuilder.RenameColumn(
                name: "SingID",
                table: "LovedSongs",
                newName: "SongID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LovedSongs",
                table: "LovedSongs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LovedSongs_SongID",
                table: "LovedSongs",
                column: "SongID");

            migrationBuilder.AddForeignKey(
                name: "FK_LovedSongs_Songs_SongID",
                table: "LovedSongs",
                column: "SongID",
                principalTable: "Songs",
                principalColumn: "SongID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LovedSongs_Songs_SongID",
                table: "LovedSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LovedSongs",
                table: "LovedSongs");

            migrationBuilder.DropIndex(
                name: "IX_LovedSongs_SongID",
                table: "LovedSongs");

            migrationBuilder.RenameColumn(
                name: "SongID",
                table: "LovedSongs",
                newName: "SingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LovedSongs",
                table: "LovedSongs",
                column: "SingID");
        }
    }
}
