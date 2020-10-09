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
    public static class FormatManager
    {
        // No properties in a static class

        // Insert new Format
        public static int Insert(Format format, bool rollback = false)
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
                    tblFormat row = new tblFormat();

                    //Set the properties
                    row.Id = dc.tblFormats.Any() ? dc.tblFormats.Max(dt => dt.Id) + 1 : 1;
                    row.Description = format.Description;

                    // Backfill Id on degreetype object (param)
                    format.Id = row.Id;
                    // Insert the row
                    dc.tblFormats.Add(row);
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

        // Update an existing Format
        public static int Update(Format format, bool rollback = false)
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
                    tblFormat row = dc.tblFormats.FirstOrDefault(dt => dt.Id == format.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.Description = format.Description;
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
        // Delete and existing Format
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
                    tblFormat row = dc.tblFormats.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblFormats.Remove(row);
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
        public static List<Format> Load()
        {
            try
            {
                List<Format> rows = new List<Format>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblFormats
                        .ToList()
                        .ForEach(dt => rows.Add(new Format
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
        public static Format LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblFormat row = dc.tblFormats.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Format format = new Format { Id = row.Id, Description = row.Description };
                        return format;
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
