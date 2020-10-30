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
    public static class OrderManager
    {
        // No properties in a static class

        // Insert new Order
        public static int Insert(Order order, bool rollback = false)
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
                    tblOrder row = new tblOrder();

                    //Set the properties
                    row.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(dt => dt.Id) + 1 : 1;
                    row.CustomerId = order.CustomerId;
                    row.UserId = order.UserId;
                    row.ShipDate = order.ShipDate;
                    row.OrderDate = order.OrderDate;

                    // Backfill Id on degreetype object (param)
                    order.Id = row.Id;
                    // Insert the row
                    dc.tblOrders.Add(row);
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

        // Update an existing Order
        public static int Update(Order order, bool rollback = false)
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
                    tblOrder row = dc.tblOrders.FirstOrDefault(dt => dt.Id == order.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.CustomerId = order.CustomerId;
                        row.OrderDate = order.OrderDate;
                        row.ShipDate = order.ShipDate;
                        row.UserId = order.UserId;
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
        // Delete and existing Order
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
                    tblOrder row = dc.tblOrders.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblOrders.Remove(row);
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
        public static List<Order> Load()
        {
            try
            {
                List<Order> rows = new List<Order>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblOrders
                        .ToList()
                        .ForEach(dt => rows.Add(new Order
                        {
                            Id = dt.Id,
                            OrderDate = dt.OrderDate,
                            ShipDate = dt.ShipDate,
                            CustomerId = dt.CustomerId,
                            UserId = dt.UserId
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
        public static Order LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblOrder row = dc.tblOrders.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Order order = new Order { Id = row.Id, ShipDate = row.ShipDate, OrderDate = row.OrderDate, CustomerId = row.CustomerId, UserId = row.UserId };
                        return order;
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
        public static Order LoadByCustomerId(int customerid)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblOrder row = dc.tblOrders.FirstOrDefault(dt => dt.CustomerId == customerid);
                    if (row != null)
                    {
                        Order order = new Order { Id = row.Id, ShipDate = row.ShipDate, OrderDate = row.OrderDate, CustomerId = row.CustomerId, UserId = row.UserId };
                        return order;
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
