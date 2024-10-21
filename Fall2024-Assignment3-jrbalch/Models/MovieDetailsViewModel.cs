namespace Fall2024_Assignment3_jrbalch.Models
{
    public class MovieDetailsViewModel
    {
        public Movie movie { get; set; }
        public List<string> reviews { get; set; }
        public IEnumerable<Actor> Actors { get; set; }

        public MovieDetailsViewModel(Movie movie, List<string> reviews, IEnumerable<Actor> actors)
        {
            this.movie = movie;
            this.reviews = reviews;
            Actors = actors;
        }
    }
}
