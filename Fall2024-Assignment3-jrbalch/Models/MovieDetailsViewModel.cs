﻿namespace Fall2024_Assignment3_jrbalch.Models
{
    public class MovieDetailsViewModel
    {
        public Movie movie { get; set; }
        public List<ReviewsViewModel> reviews { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public string? OverallSentiment { get; set; }

        public MovieDetailsViewModel(Movie movie, List<ReviewsViewModel> reviews, IEnumerable<Actor> actors, string overallSentiment)
        {
            this.movie = movie;
            this.reviews = reviews;
            Actors = actors;
            OverallSentiment = overallSentiment;
        }
    }
}
