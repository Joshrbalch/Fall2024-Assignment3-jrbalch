using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_jrbalch.Models;

namespace Fall2024_Assignment3_jrbalch.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Fall2024_Assignment3_jrbalch.Models.Movie> Movie { get; set; } = default!;
        public DbSet<Fall2024_Assignment3_jrbalch.Models.Actor> Actor { get; set; } = default!;
        public DbSet<Fall2024_Assignment3_jrbalch.Models.ActorMovie> ActorMovie { get; set; } = default!;
    }
}
