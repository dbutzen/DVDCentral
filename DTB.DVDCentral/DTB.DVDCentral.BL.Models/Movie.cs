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
        public int FormatId { get; set; }
        public int DirectorId { get; set; }
        [DisplayName("Quantity In Stock")]
        public int InStkQty { get; set; }
        [DisplayName("Image")] 
        public string ImagePath { get; set; }
        [DisplayName("Director")]
        public string DirectorName { get; set; }
        [DisplayName("Format")]
        public string FormatName { get; set; }
        [DisplayName("Rating")]
        public string RatingName { get; set; }

    }
}
