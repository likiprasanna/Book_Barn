using Rating.Domain.Repositories;
using Rating.Infrastructure.Repositories;
using Rating.Data; // Include your namespace for RatingDbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register RatingDbContext with the DI container
builder.Services.AddDbContext<RatingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Adjust connection string

// Register your repository
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
