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
    public static class DirectorManager
    {
        // No properties in a static class

        // Insert new Director
        public static int Insert(Director director, bool rollback = false)
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
                    tblDirector row = new tblDirector();

                    //Set the properties
                    row.Id = dc.tblDirectors.Any() ? dc.tblDirectors.Max(dt => dt.Id) + 1 : 1;
                    row.FirstName = director.FirstName;
                    row.LastName = director.LastName;

                    // Backfill Id on degreetype object (param)
                    director.Id = row.Id;
                    // Insert the row
                    dc.tblDirectors.Add(row);
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

        // Update an existing Director
        public static int Update(Director director, bool rollback = false)
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
                    tblDirector row = dc.tblDirectors.FirstOrDefault(dt => dt.Id == director.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.FirstName = director.FirstName;
                        row.LastName = director.LastName;
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
        // Delete and existing Director
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
                    tblDirector row = dc.tblDirectors.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblDirectors.Remove(row);
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
        public static List<Director> Load()
        {
            try
            {
                List<Director> rows = new List<Director>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblDirectors
                        .ToList()
                        .ForEach(dt => rows.Add(new Director
                        {
                            Id = dt.Id,
                            FirstName = dt.FirstName,
                            LastName = dt.LastName
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
        public static Director LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblDirector row = dc.tblDirectors.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Director director = new Director { Id = row.Id, FirstName = row.FirstName, LastName = row.LastName };
                        return director;
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
