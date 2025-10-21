using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToCinemaaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Cinemaas (name, imgPath) values ('Yakidoo', '9.png');insert into Cinemaas (name, imgPath) values ('Pixope', '8.png');insert into Cinemaas (name, imgPath) values ('LiveZ', '6.png');insert into Cinemaas (name, imgPath) values ('Muxo', '1.png');insert into Cinemaas (name, imgPath) values ('Roodel', '2.png');insert into Cinemaas (name, imgPath) values ('Dabshots', '7.png');insert into Cinemaas (name, imgPath) values ('Thoughtblab', '4.png');insert into Cinemaas (name, imgPath) values ('Dazzlesphere', '10.png');insert into Cinemaas (name, imgPath) values ('Browsetype', '3.png');insert into Cinemaas (name, imgPath) values ('Gabcube', '5.png');");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Cinemaas");
        }
    }
}
