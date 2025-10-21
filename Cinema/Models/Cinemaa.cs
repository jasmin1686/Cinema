using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    [Table("Cinemaas")]
    public class Cinemaa
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string? ImgPath { get; set; }
        
    }
}
