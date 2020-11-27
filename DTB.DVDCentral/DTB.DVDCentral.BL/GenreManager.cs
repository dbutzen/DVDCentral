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
    public static class GenreManager
    {
        // No properties in a static class

        // Insert new Genre
        public static int Insert(Genre genre, bool rollback = false)
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
                    tblGenre row = new tblGenre();

                    //Set the properties
                    row.Id = dc.tblGenres.Any() ? dc.tblGenres.Max(dt => dt.Id) + 1 : 1;
                    row.Description = genre.Description;

                    // Backfill Id on degreetype object (param)
                    genre.Id = row.Id;
                    // Insert the row
                    dc.tblGenres.Add(row);
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

        // Update an existing Genre
        public static int Update(Genre genre, bool rollback = false)
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
                    tblGenre row = dc.tblGenres.FirstOrDefault(dt => dt.Id == genre.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = genre.Description;
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
        // Delete and existing Genre
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
                    tblGenre row = dc.tblGenres.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblGenres.Remove(row);
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
        public static List<Genre> Load()
        {
            using (butzendbEntities dc = new butzendbEntities())
            {
                List<Genre> genres = new List<Genre>();


                dc.tblGenres.ToList().ForEach(g => genres.Add(new Genre { Id = g.Id, Description = g.Description }));
                return genres;


            }
        }
        public static List<Genre> Load(int movieId)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    List<Genre> genres = new List<Genre>();

                    var results = (from a in dc.tblGenres
                                   join pda in dc.tblMovieGenres on a.Id equals pda.GenreId
                                   where pda.MovieId == movieId
                                   select new
                                   {
                                       a.Id,
                                       a.Description
                                   }).ToList();

                    results.ForEach(g => genres.Add(new Genre { Id = g.Id, Description = g.Description }));

                    return genres;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // Retrieve one degree type
        public static Genre LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblGenre row = dc.tblGenres.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Genre genre = new Genre { Id = row.Id, Description = row.Description };
                        return genre;
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
