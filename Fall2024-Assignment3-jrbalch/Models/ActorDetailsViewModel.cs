namespace Fall2024_Assignment3_jrbalch.Models
{
    public class ActorDetailsViewModel
    {
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public List<string> Reviews { get; set; }

        public ActorDetailsViewModel(Actor actor, IEnumerable<Movie> movies, List<string> reviews)
        {
            Actor = actor;
            Movies = movies;
            Reviews = reviews;
        }
    }
}
