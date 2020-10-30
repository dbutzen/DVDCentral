using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
    {
        protected butzendbEntities dc;
        protected DbContextTransaction transaction;
        [TestInitialize]
        public void Initialize()
        {
            dc = new butzendbEntities();
            transaction = dc.Database.BeginTransaction();
        }
        [TestCleanup]
        public void TransactionCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
        }
        [TestMethod]
        public void LoadOrderItemsTest()
        {


            int expected = 4;
            int actual = 0;

            var orderItems = dc.tblOrderItems;

            actual = orderItems.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }
        /*
        [TestMethod]
        public void LoadOrderItemsLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblOrderItem
            var orderItems = from dt in dc.tblOrderItems
                              select dt;

            actual = orderItems.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);
            dc = null;

        }*/

        [TestMethod]
        public void InsertTest()
        {

            // dc only exists in here
            // type = 1 row, types = all rows

            //make new row
            tblOrderItem newrow = new tblOrderItem();

            //set column values
            newrow.Id = -98;
            newrow.MovieId = 1;
            newrow.OrderId = 1;
            newrow.Quantity = 1;
            newrow.Cost = 1;

            // Insert of the row
            dc.tblOrderItems.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }

        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblOrderItem where id = -99
            tblOrderItem existingOrderItem = (from dt in dc.tblOrderItems
                                      where dt.Id == 1
                                      select dt).FirstOrDefault();

            if (existingOrderItem != null)
            {
                //update description
                existingOrderItem.MovieId = 1;
                existingOrderItem.OrderId = 1;
                existingOrderItem.Quantity = 1;
                existingOrderItem.Cost = 1;
                dc.SaveChanges();
            }

            tblOrderItem updatedOrderItem = (from dt in dc.tblOrderItems
                                     where dt.Id == 1
                                     select dt).FirstOrDefault();

            Assert.AreEqual(existingOrderItem.MovieId, updatedOrderItem.MovieId);

        }

        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblOrderItem where id = -99
            tblOrderItem existingOrderItem = (from dt in dc.tblOrderItems
                                      where dt.Id == -98
                                      select dt).FirstOrDefault();

            if (existingOrderItem != null)
            {
                //update description
                dc.tblOrderItems.Remove(existingOrderItem);
                dc.SaveChanges();
            }

            tblOrderItem deletedOrderItem = (from dt in dc.tblOrderItems
                                     where dt.Id == -98
                                     select dt).FirstOrDefault();

            Assert.IsNull(deletedOrderItem);

        }
    }
}
