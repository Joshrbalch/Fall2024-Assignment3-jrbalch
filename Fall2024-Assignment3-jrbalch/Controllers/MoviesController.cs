using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_jrbalch.Data;
using Fall2024_Assignment3_jrbalch.Models;
using static Fall2024_Assignment3_jrbalch.Services.OpenAIService;
using VaderSharp2;

using Fall2024_Assignment3_jrbalch.Services;
using Fall2024_Assignment3_jrbalch.Data.Migrations;

namespace Fall2024_Assignment3_jrbalch.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly OpenAIService _openAIService;

        public MoviesController(ApplicationDbContext context, OpenAIService openAIService)
        {
            _openAIService = openAIService;
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            var actors = await _context.ActorMovie
                .Include(cs => cs.Actor)
                .Where(cs => cs.MovieId == movie.ID)
                .Select(cs => cs.Actor)
                .ToListAsync();

            List<string> reviews = await _openAIService.GenerateMovieReviewsAsync(movie.Title);

            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();

            List<MovieReviewsViewModel> reviewViewModels = new List<MovieReviewsViewModel>();

            int positiveCount = 0;
            int negativeCount = 0;

            foreach (var review in reviews)
            {
                var sentimentResult = analyzer.PolarityScores(review);
                string sentiment;

                if (sentimentResult.Compound >= 0.05)
                {
                    sentiment = "Positive";
                    positiveCount++;
                }
                else if (sentimentResult.Compound <= -0.05)
                {
                    sentiment = "Negative";
                    negativeCount++;
                }
                else
                {
                    sentiment = "Neutral";
                }

                reviewViewModels.Add(new MovieReviewsViewModel
                {
                    Review = review,
                    Sentiment = sentiment
                });
            }

            // Calculate overall sentiment
            string overallSentiment = positiveCount > negativeCount ? "Positive" : "Negative";

            MovieDetailsViewModel vm = new MovieDetailsViewModel(movie, reviewViewModels, actors, overallSentiment);

            return View(vm);
        }


        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Genre,Year,Director,Actors,Poster")] Movie movie)
        {
            var poster = Request.Form.Files["Poster"];
            if (ModelState.IsValid)
            {
                if (poster != null && poster.Length > 0)
                {
                    using var memoryStream = new MemoryStream(); // Dispose() for garbage collection 
                    await poster.CopyToAsync(memoryStream);
                    movie.Poster = memoryStream.ToArray();
                }
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public IActionResult GetImage(int id)
        {
            var movie = _context.Movie.Find(id);
            if (movie == null || movie.Poster == null)
            {
                return NotFound(); // Or return a default image
            }
            return File(movie.Poster, "image/jpeg"); // Assuming JPEG, adjust MIME type if necessary
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,IMDBLink,Genre,Year,Actors,Poster")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
