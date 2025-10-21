namespace Cinema.Models
{
    public class MovieSubimg
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
