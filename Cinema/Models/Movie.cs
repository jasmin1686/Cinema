
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public DateTime DateTime { get; set; }
        public string? MainImg { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int CinemaId { get; set; }
        public Cinemaa? Cinemaa { get; set; }

        public ICollection<Actormovie>? Actormovies { get; set; }

    }
}
