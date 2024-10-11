namespace Fall2024_Assignment3_jrbalch.Models
{
    public class ActorMoviesViewModel
    {
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }

        public ActorMoviesViewModel(Actor actor, IEnumerable<Movie> movies)
        {
            Actor = actor;
            Movies = movies;
        }
    }
}
