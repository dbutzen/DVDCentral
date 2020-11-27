using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.DVDCentral.BL.Models;
using DTB.DVDCentral.PL;

namespace DTB.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static void Delete(int progdecid, int genreid)
        {
            using (butzendbEntities dc = new butzendbEntities())
            {
                tblMovieGenre pda = dc.tblMovieGenres.FirstOrDefault(p => p.MovieId == progdecid
                                        && p.GenreId == genreid);
                if (pda != null)
                {
                    dc.tblMovieGenres.Remove(pda);
                    dc.SaveChanges();
                }
            }
        }

        public static void Add(int progdecid, int genreid)
        {
            using (butzendbEntities dc = new butzendbEntities())
            {
                tblMovieGenre pda = new tblMovieGenre();
                pda.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(p => p.Id) + 1 : 1;
                pda.MovieId = progdecid;
                pda.GenreId = genreid;

                dc.tblMovieGenres.Add(pda);
                dc.SaveChanges();
            }
        }

        public static List<Movie> Load(int genreId)
        {
            try
            {
                List<Movie> rows = new List<Movie>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    var movies = (from m in dc.tblMovies
                                  join mr in dc.tblRatings on m.RatingId equals mr.Id
                                  join f in dc.tblFormats on m.FormatId equals f.Id
                                  join d in dc.tblDirectors on m.DirectorId equals d.Id
                                  join mg in dc.tblMovieGenres on m.Id equals mg.MovieId
                                  join g in dc.tblGenres on mg.GenreId equals g.Id
                                  where (g.Id == genreId)
                                  orderby m.Title
                                  select new
                                  {
                                      MovieId = m.Id,
                                      RatingId = mr.Id,
                                      ratingName = mr.Description,
                                      FormatId = f.Id,
                                      formatName = f.Description,
                                      DirectorId = d.Id,
                                      d.FirstName,
                                      d.LastName,
                                      m.Title,
                                      m.Cost,
                                      m.Description,
                                      m.ImagePath,
                                      m.InStkQty
                                  }).ToList();

                    movies.ForEach(m => rows.Add(new Movie
                    {
                        Id = m.MovieId,
                        Title = m.Title,
                        RatingId = m.RatingId,
                        RatingName = m.ratingName,
                        FormatId = m.FormatId,
                        FormatName = m.formatName,
                        DirectorId = m.DirectorId,
                        DirectorName = m.LastName + ", " + m.FirstName,
                        Cost = m.Cost,
                        Description = m.Description,
                        ImagePath = m.ImagePath,
                        InStkQty = m.InStkQty

                    }));
                    return rows;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
