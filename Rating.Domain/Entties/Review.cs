using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain.Entties
{

    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        public int BookId { get; set; }

        public int UserId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime RatedDate { get; set; }

    }
}
