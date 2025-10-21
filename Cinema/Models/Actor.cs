namespace Cinema.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string? Img { get; set; }
       
        public ICollection<Actormovie>? Actormovies { get; set; }

    }
}
