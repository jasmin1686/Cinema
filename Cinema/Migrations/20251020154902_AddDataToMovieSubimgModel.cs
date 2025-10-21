using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToMovieSubimgModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into MovieSubimgs (image, movieId) values ('6.png', 11);insert into MovieSubimgs (image, movieId) values ('4.png', 12);insert into MovieSubimgs (image, movieId) values ('7.png', 13);insert into MovieSubimgs (image, movieId) values ('9.png', 14);insert into MovieSubimgs (image, movieId) values ('5.png', 15);insert into MovieSubimgs (image, movieId) values ('5.png', 16);insert into MovieSubimgs (image, movieId) values ('2.png', 17);insert into MovieSubimgs (image, movieId) values ('10.png', 18);insert into MovieSubimgs (image, movieId) values ('7.png', 19);insert into MovieSubimgs (image, movieId) values ('4.png', 20);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE MovieSubimgs");
        }
    }
}
