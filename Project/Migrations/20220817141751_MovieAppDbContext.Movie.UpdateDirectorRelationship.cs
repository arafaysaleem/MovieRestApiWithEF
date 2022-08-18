using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRestApiWithEF.Migrations
{
    public partial class MovieAppDbContextMovieUpdateDirectorRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieWorker_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "MovieWorker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieWorker_DirectorId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies");
        }
    }
}
