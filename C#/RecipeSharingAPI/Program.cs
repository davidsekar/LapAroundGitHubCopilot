using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Create new SQLite connection
var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

// Register DbContext with InMemoryDatabase
builder.Services.AddDbContext<RecipeSharingDbContext>(options =>
    options.UseSqlite(connection));

// Register IRecipeService with RecipeService implementation
builder.Services.AddTransient<IRecipeService, RecipeService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RecipeSharingDbContext>();
    dbContext.Database.OpenConnection();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();