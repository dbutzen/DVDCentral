using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.DVDCentral.PL;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.BL
{
    public static class MovieManager
    {
        // No properties in a static class

        // Insert new Movie
        public static int Insert(Movie movie, bool rollback = false)
        {
            // Insert a row
            try
            {
                int results = 0;
                using (butzendbEntities dc = new butzendbEntities()) 
                {                                                   
                    DbContextTransaction transaction = null;         
                    if (rollback) transaction = dc.Database.BeginTransaction(); 

                    //Make a new row
                    tblMovie row = new tblMovie();

                    //Set the properties
                    row.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(dt => dt.Id) + 1 : 1;
                    row.Description = movie.Description;
                    row.Cost = movie.Cost;
                    row.DirectorId = movie.DirectorId;
                    row.FormatId = movie.FormatId;
                    row.ImagePath = movie.ImagePath;
                    row.RatingId = movie.RatingId;
                    row.Title = movie.Title;
                    row.InStkQty = movie.InStkQty;

                    // Backfill Id on degreetype object (param)
                    movie.Id = row.Id;
                    // Insert the row
                    dc.tblMovies.Add(row);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Update an existing Movie
        public static int Update(Movie movie, bool rollback = false)
        {
            // Update the row
            try
            {
                int results;
                using (butzendbEntities dc = new butzendbEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    //Make a new row
                    tblMovie row = dc.tblMovies.FirstOrDefault(dt => dt.Id == movie.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = movie.Description;
                        row.Cost = movie.Cost;
                        row.DirectorId = movie.DirectorId;
                        row.FormatId = movie.FormatId;
                        row.ImagePath = movie.ImagePath;
                        row.RatingId = movie.RatingId;
                        row.Title = movie.Title;
                        row.InStkQty = movie.InStkQty;
                        results = dc.SaveChanges();

                        // Insert the row
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }

                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Delete and existing Movie
        public static int Delete(int id, bool rollback = false)
        {
            // delete a row
            try
            {
                int results;
                using (butzendbEntities dc = new butzendbEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    //Make a new row
                    tblMovie row = dc.tblMovies.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblMovies.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<Movie> Load()
        {
            return Load(null);
        }
        // Retrieve all the degree types
        public static List<Movie> Load(int? directorId)
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
                                  where (m.DirectorId == directorId || directorId == null)
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
                            DirectorName = m.LastName +", "+m.FirstName,
                            Cost = m.Cost,
                            Description = m.Description,
                            ImagePath = m.ImagePath,
                            InStkQty = m.InStkQty

                        })) ;
                    return rows;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Retrieve one degree type
        public static Movie LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    var movie = (from m in dc.tblMovies
                                  join mr in dc.tblRatings on m.RatingId equals mr.Id
                                  join f in dc.tblFormats on m.FormatId equals f.Id
                                  join d in dc.tblDirectors on m.DirectorId equals d.Id
                                  where m.Id == id
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
                                  }).FirstOrDefault();
                    if (movie != null)
                    {
                        Movie m = new Movie
                        {
                            Id = movie.MovieId,
                            RatingId = movie.RatingId,
                            RatingName = movie.ratingName,
                            FormatId = movie.FormatId,
                            FormatName = movie.formatName,
                            DirectorId = movie.DirectorId,
                            DirectorName = movie.LastName + ", " + movie.FirstName,
                            Title = movie.Title,
                            Cost = movie.Cost,
                            Description = movie.Description,
                            ImagePath = movie.ImagePath,
                            InStkQty = movie.InStkQty
                        };
                        return m;
                    }
                    else
                    {
                        throw new Exception("Row was not found, sorry!");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
