using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRestApiWithEF.Migrations
{
    public partial class MovieAppDbContextMovieUpdateWorkerRelationshipConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Movies_MovieId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieWorker_DirectorId",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Movies_MovieId",
                table: "MovieCast",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieWorker_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "MovieWorker",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Movies_MovieId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieWorker_DirectorId",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Movies_MovieId",
                table: "MovieCast",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieWorker_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "MovieWorker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
