using Fall2024_Assignment3_jrbalch.Models;
using Fall2024_Assignment3_jrbalch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fall2024_Assignment3_jrbalch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OpenAIService _openAIService;

        // Constructor - assign the injected OpenAIService to the _openAIService field
        public HomeController(ILogger<HomeController> logger, OpenAIService openAIService)
        {
            _logger = logger;
            _openAIService = openAIService; // This line was missing!
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Description(string movieTitle)
        {
            // Ensure that OpenAIService is used properly here
            var reviews = await _openAIService.GenerateReviewsAsync("Kung Fu Panda 2");
            //ViewData["Reviews"] = reviews;
            return View(reviews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
