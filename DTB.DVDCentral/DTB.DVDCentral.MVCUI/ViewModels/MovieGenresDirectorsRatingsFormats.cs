using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTB.DVDCentral.MVCUI.ViewModels
{
    public class MovieGenresDirectorsRatingsFormats
    {
        public BL.Models.Movie Movie { get; set; }

        public List<BL.Models.Genre> Genres { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
        public List<BL.Models.Director> Directors { get; set; }
        public List<BL.Models.Rating> Ratings { get; set; }
        public List<BL.Models.Format> Formats { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}