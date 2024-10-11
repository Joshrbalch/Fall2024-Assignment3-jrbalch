using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Fall2024_Assignment3_jrbalch.Models
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? IMDBLink { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public string? Actors { get; set; }
        public byte[]? Poster { get; set; }
    }
}
