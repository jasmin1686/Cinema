namespace Cinema.ViewModels
{
    public class ActorVM
    {
        
            public string Name { get; set; }
            public IFormFile? Img { get; set; }
        public int MovieId { get; set; }

        public List<int>? SelectedMovies { get; set; }
        

    }
}
