using Fall2024_Assignment3_jrbalch.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Azure.AI.OpenAI;
using Microsoft.Build.Framework;
using OpenAI.Chat;
using NuGet.ProjectModel;
using Fall2024_Assignment3_jrbalch.Services;
using static Fall2024_Assignment3_jrbalch.Services.OpenAIService;
using Fall2024_Assignment3_jrbalch.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<OpenAIService>();

var openAIEndpoint = builder.Configuration["OpenAIEndpoint"];
var openAIKey = builder.Configuration["OpenAIKey"];

OpenAIService openAIService = new OpenAIService(builder.Configuration);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

await SeedData(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var OpenAIKey = app.Configuration["OpenAIKey"];

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Run();

async Task SeedData(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Seed Movies
        if (!context.Movies.Any())
        {
            context.Movies.AddRange(
                new Movie
                {
                    Title = "Inception",
                    IMDBLink = "https://www.imdb.com/title/tt1375666/",
                    Genre = "Sci-Fi",
                    Year = 2010,
                    Actors = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
                    Poster = null
                },
                new Movie
                {
                    Title = "The Matrix",
                    IMDBLink = "https://www.imdb.com/title/tt0133093/",
                    Genre = "Action",
                    Year = 1999,
                    Actors = "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss",
                    Poster = null
                },
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    IMDBLink = "https://www.imdb.com/title/tt0111161/",
                    Genre = "Drama",
                    Year = 1994,
                    Actors = "Tim Robbins, Morgan Freeman, Bob Gunton",
                    Poster = null
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    IMDBLink = "https://www.imdb.com/title/tt0468569/",
                    Genre = "Action",
                    Year = 2008,
                    Poster = null
                }
            );

            await context.SaveChangesAsync();
        }

        if (!context.Actor.Any())
        {
            context.Actor.AddRange(
                new Actor { Name = "Tim Robbins", Gender = "Male", Age = 65, IMDBLink = "https://www.imdb.com/name/nm0000600/" },
                new Actor { Name = "Morgan Freeman", Gender = "Male", Age = 87, IMDBLink = "https://www.imdb.com/name/nm0000151/" },
                new Actor { Name = "Leonardo DiCaprio", Gender = "Male", Age = 49, IMDBLink = "https://www.imdb.com/name/nm0000138/" },
                new Actor { Name = "Natalie Portman", Gender = "Female", Age = 43, IMDBLink = "https://www.imdb.com/name/nm0000204/" },
                new Actor { Name = "Keanu Reeves", Gender = "Male", Age = 59, IMDBLink = "https://www.imdb.com/name/nm0000206/" },
                new Actor { Name = "Scarlett Johansson", Gender = "Female", Age = 39, IMDBLink = "https://www.imdb.com/name/nm0424060/" },
                new Actor { Name = "Denzel Washington", Gender = "Male", Age = 69, IMDBLink = "https://www.imdb.com/name/nm0000243/" },
                new Actor { Name = "Emma Stone", Gender = "Female", Age = 35, IMDBLink = "https://www.imdb.com/name/nm1695364/" }
            );

            await context.SaveChangesAsync();
        }
    }
}

