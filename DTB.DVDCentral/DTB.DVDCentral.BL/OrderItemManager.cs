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
    public static class OrderItemManager
    {
        // No properties in a static class

        // Insert new OrderItem
        
        public static int Insert(OrderItem orderItem, bool rollback = false)
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
                    tblOrderItem row = new tblOrderItem();

                    //Set the properties
                    row.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(dt => dt.Id) + 1 : 1;
                    row.OrderId = orderItem.OrderId;
                    row.MovieId = orderItem.MovieId;
                    row.Cost = (decimal)orderItem.Cost;
                    row.Quantity = orderItem.Quantity;

                    // Backfill Id on degreetype object (param)
                    orderItem.Id = row.Id;
                    // Insert the row
                    dc.tblOrderItems.Add(row);
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
        

        

        // Update an existing OrderItem
        public static int Update(OrderItem orderItem, bool rollback = false)
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
                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(dt => dt.Id == orderItem.Id);

                    if (row != null)
                    {
                        //Set the properties
                        row.OrderId = orderItem.OrderId;
                        row.MovieId = orderItem.MovieId;
                        row.Cost = (decimal)orderItem.Cost;
                        row.Quantity = orderItem.Quantity;
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
        // Delete and existing OrderItem
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
                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblOrderItems.Remove(row);
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
        /*public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> rows = new List<OrderItem>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblOrderItems
                        .ToList()
                        .ForEach(dt => rows.Add(new OrderItem
                        {
                            Id = dt.Id,
                            OrderId = dt.OrderId,
                            MovieId = dt.MovieId,
                            Cost = (double)dt.Cost,
                            Quantity = dt.Quantity
                }));
                    return rows;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/
        // Retrieve one degree type
        public static OrderItem LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        OrderItem orderItem = new OrderItem { Id = row.Id, OrderId = row.OrderId, MovieId = row.MovieId, Cost = (double)row.Cost, Quantity = row.Quantity };
                        return orderItem;
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

        public static OrderItem LoadByOrderId(int orderid)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(dt => dt.OrderId == orderid);
                    if (row != null)
                    {
                        OrderItem orderItem = new OrderItem { Id = row.Id, OrderId = row.OrderId, MovieId = row.MovieId, Cost = (double)row.Cost, Quantity = row.Quantity };
                        return orderItem;
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
