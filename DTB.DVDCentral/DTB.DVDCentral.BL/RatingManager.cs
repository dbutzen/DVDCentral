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
    public static class RatingManager
    {
        // No properties in a static class

        // Insert new Rating
        public static int Insert(Rating rating, bool rollback = false)
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
                    tblRating row = new tblRating();

                    //Set the properties
                    row.Id = dc.tblRatings.Any() ? dc.tblRatings.Max(dt => dt.Id) + 1 : 1;
                    row.Description = rating.Description;

                    // Backfill Id on degreetype object (param)
                    rating.Id = row.Id;
                    // Insert the row
                    dc.tblRatings.Add(row);
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

        // Update an existing Rating
        public static int Update(Rating rating, bool rollback = false)
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
                    tblRating row = dc.tblRatings.FirstOrDefault(dt => dt.Id == rating.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = rating.Description;
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
        // Delete and existing Rating
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
                    tblRating row = dc.tblRatings.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblRatings.Remove(row);
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
        public static List<Rating> Load()
        {
            try
            {
                List<Rating> rows = new List<Rating>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblRatings
                        .ToList()
                        .ForEach(dt => rows.Add(new Rating
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
        public static Rating LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblRating row = dc.tblRatings.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Rating rating = new Rating { Id = row.Id, Description = row.Description };
                        return rating;
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
