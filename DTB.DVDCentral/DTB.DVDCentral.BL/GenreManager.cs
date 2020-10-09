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
                using (butzendbEntities dc = new butzendbEntities()) // I'm not sure why this is the required syntax and not "DVDCentralEntities
                {                                                    // If you can please let me know what I did wrong, my best guess has to do with
                    DbContextTransaction transaction = null;         // something screwy when I created the database as there's a DVDCentral.DB and
                    if (rollback) transaction = dc.Database.BeginTransaction(); // a DTB.DVDCentral.BL.DB in my local projects

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
            try
            {
                List<Genre> rows = new List<Genre>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblGenres
                        .ToList()
                        .ForEach(dt => rows.Add(new Genre
                        {
                            Id = dt.Id,
                            Description = dt.Description
                        }));
                    return rows;
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
