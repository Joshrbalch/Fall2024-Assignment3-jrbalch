using System.Collections;
using System.Data.Entity;

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

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie>? Movies { get; set; }
    }
}
