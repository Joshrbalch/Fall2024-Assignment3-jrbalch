using System.Collections;

namespace Fall2024_Assignment3_jrbalch.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? IMDBLink { get; set; }
        public int? releaseYear { get; set; }
        public BitArray? Poster { get; set; }
    }
}
