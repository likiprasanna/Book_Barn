using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Rating.Domain.Entties
{
    public class AverageRating
    {
        [Key]
        public int AvgRatingId { get; set; }
        public int BookId { get; set; }
        public double AvgRating { get; set; }
        public int TotalReview { get; set; }
    }
}
