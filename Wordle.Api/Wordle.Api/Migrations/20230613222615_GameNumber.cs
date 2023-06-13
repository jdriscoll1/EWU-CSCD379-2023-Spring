using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Api.Migrations
{
    /// <inheritdoc />
    public partial class GameNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gameID",
                table: "Games",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "Games",
                newName: "GameNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Games",
                newName: "gameID");

            migrationBuilder.RenameColumn(
                name: "GameNumber",
                table: "Games",
                newName: "_id");
        }
    }
}
