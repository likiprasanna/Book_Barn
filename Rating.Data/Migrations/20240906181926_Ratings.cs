using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rating.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AverageRating",
                columns: table => new
                {
                    AvgRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AvgRating = table.Column<double>(type: "float", nullable: false),
                    TotalReview = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AverageRating", x => x.AvgRatingId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    RatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.InsertData(
                table: "AverageRating",
                columns: new[] { "AvgRatingId", "AvgRating", "BookId", "TotalReview" },
                values: new object[,]
                {
                    { 1, 4.5, 1001, 2 },
                    { 2, 5.0, 1002, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "BookId", "RatedDate", "Rating", "Review", "UserId" },
                values: new object[,]
                {
                    { 1, 1001, new DateTime(2024, 8, 27, 23, 49, 25, 902, DateTimeKind.Local).AddTicks(1601), 5, "Great book on C# programming.", 2001 },
                    { 2, 1001, new DateTime(2024, 8, 29, 23, 49, 25, 902, DateTimeKind.Local).AddTicks(1633), 4, "Informative but a bit lengthy.", 2002 },
                    { 3, 1002, new DateTime(2024, 9, 1, 23, 49, 25, 902, DateTimeKind.Local).AddTicks(1637), 5, "Excellent introduction to algorithms.", 2003 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AverageRating");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
