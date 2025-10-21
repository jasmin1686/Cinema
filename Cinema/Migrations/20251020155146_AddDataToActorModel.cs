using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToActorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Actors (name, img) values ('Hale Fardy', '12.png');insert into Actors (name, img) values ('Helli Milier', '15.png');insert into Actors (name, img) values ('Nolana Pietzker', '6.png');insert into Actors (name, img) values ('Fayth Shetliff', '19.png');insert into Actors (name, img) values ('Roddy Casillis', '10.png');insert into Actors (name, img) values ('Ulysses Curr', '10.png');insert into Actors (name, img) values ('Bernarr Atwool', '10.png');insert into Actors (name, img) values ('Noellyn Leavey', '10.png');insert into Actors (name, img) values ('Tonie Shevill', '17.png');insert into Actors (name, img) values ('Stillman Painter', '2.png');insert into Actors (name, img) values ('Gaylord OIlier', '11.png');insert into Actors (name, img) values ('Skye Amorine', '11.png');insert into Actors (name, img) values ('Kirsten Dowding', '10.png');insert into Actors (name, img) values ('Derry Idel', '8.png');insert into Actors (name, img) values ('Alley Serjeantson', '6.png');insert into Actors (name, img) values ('Inger De Micoli', '1.png');insert into Actors (name, img) values ('Cleopatra Dorran', '10.png');insert into Actors (name, img) values ('Hazlett Stove', '6.png');insert into Actors (name, img) values ('Kelley Dalliston', '5.png');insert into Actors (name, img) values ('Fabe Hollyman', '13.png');insert into Actors (name, img) values ('Catlin Goodanew', '14.png');insert into Actors (name, img) values ('Elysee Chritchley', '9.png');insert into Actors (name, img) values ('Thorndike Cheak', '11.png');insert into Actors (name, img) values ('Wildon Turfin', '1.png');insert into Actors (name, img) values ('Elwin Sawnwy', '7.png');insert into Actors (name, img) values ('Katine Bertelsen', '15.png');insert into Actors (name, img) values ('Audy Hercock', '14.png');insert into Actors (name, img) values ('Gerick Randalston', '18.png');insert into Actors (name, img) values ('Drew Durbyn', '13.png');insert into Actors (name, img) values ('Norbie Hawler', '3.png');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Actors");
        }
    }
}
