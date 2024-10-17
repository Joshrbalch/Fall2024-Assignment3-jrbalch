namespace Fall2024_Assignment3_jrbalch.Models
{
    public class MovieReviewViewModel
    {
        public Movie movie { get; set; }
        public List<string> reviews { get; set; }

        public MovieReviewViewModel(Movie movie, List<string> reviews)
        {
            this.movie = movie;
            this.reviews = reviews;
        }
    }
}
