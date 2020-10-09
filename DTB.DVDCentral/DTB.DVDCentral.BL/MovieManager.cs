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
                using (butzendbEntities dc = new butzendbEntities()) // I'm not sure why this is the required syntax and not "DVDCentralEntities
                {                                                    // If you can please let me know what I did wrong, my best guess has to do with
                    DbContextTransaction transaction = null;         // something screwy when I created the database as there's a DVDCentral.DB and
                    if (rollback) transaction = dc.Database.BeginTransaction(); // a DTB.DVDCentral.BL.DB in my local projects

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
        // Retrieve all the degree types
        public static List<Movie> Load()
        {
            try
            {
                List<Movie> rows = new List<Movie>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblMovies
                        .ToList()
                        .ForEach(dt => rows.Add(new Movie
                        {
                            Id = dt.Id,
                            Description = dt.Description,
                            Title = dt.Title,
                            Cost = dt.Cost,
                            InStkQty = dt.InStkQty,
                            DirectorId = dt.DirectorId,
                            FormatId = dt.FormatId,
                            ImagePath = dt.ImagePath,
                            RatingId = dt.RatingId

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
                    tblMovie row = dc.tblMovies.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Movie movie = new Movie { Id = row.Id, Description = row.Description, Title = row.Title, Cost = row.Cost, InStkQty = row.InStkQty,
                        DirectorId = row.DirectorId, FormatId = row.FormatId, ImagePath = row.ImagePath, RatingId = row.RatingId};
                        return movie;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
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
