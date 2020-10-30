using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.DVDCentral.BL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        [DisplayName("Rating")]
        public int RatingId { get; set; }
        [DisplayName("Format")]
        public int FormatId { get; set; }
        [DisplayName("Director")]
        public int DirectorId { get; set; }
        [DisplayName("Quantity In Stock")]
        public int InStkQty { get; set; }
        public string ImagePath { get; set; }
    }
}
