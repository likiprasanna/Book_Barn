using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Rating.Domain.Entties;

namespace Rating.Data
{
    public class RatingDbContext : DbContext
    {
        public RatingDbContext()
        {

        }

        public RatingDbContext(DbContextOptions<RatingDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=BookBarnReview;Integrated Security=True;Encrypt=True ");
            }
        }
        
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<AverageRating> AverageRating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Reviews Data
            modelBuilder.Entity<Reviews>().HasData(
                new Reviews
                {
                    ReviewId = 1,
                    BookId = 1001,
                    UserId = 2001,
                    Review = "Great book on C# programming.",
                    Rating = 5,
                    RatedDate = DateTime.Now.AddDays(-10)
                },
                new Reviews
                {
                    ReviewId = 2,
                    BookId = 1001,
                    UserId = 2002,
                    Review = "Informative but a bit lengthy.",
                    Rating = 4,
                    RatedDate = DateTime.Now.AddDays(-8)
                },
                new Reviews
                {
                    ReviewId = 3,
                    BookId = 1002,
                    UserId = 2003,
                    Review = "Excellent introduction to algorithms.",
                    Rating = 5,
                    RatedDate = DateTime.Now.AddDays(-5)
                }
            );

            // Seed AverageRating Data
            modelBuilder.Entity<AverageRating>().HasData(
                new AverageRating
                {
                    AvgRatingId = 1,
                    BookId = 1001,
                    AvgRating = 4.5,
                    TotalReview = 2
                },
                new AverageRating
                {
                    AvgRatingId = 2,
                    BookId = 1002,
                    AvgRating = 5.0,
                    TotalReview = 1
                }
            );
        }
    }
}

