namespace Fall2024_Assignment3_jrbalch.Models
{
    public class ActorDetailsViewModel
    {
        public Actor Actor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public List<ReviewsViewModel> Reviews { get; set; }
        public string? OverallSentiment { get; set; }

        public ActorDetailsViewModel(Actor actor, IEnumerable<Movie> movies, List<ReviewsViewModel> reviews, string overallSentiment)
        {
            Actor = actor;
            Movies = movies;
            Reviews = reviews;
            OverallSentiment = overallSentiment;
        }
    }
}
