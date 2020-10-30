using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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
        public void LoadOrdersTest()
        {


            int expected = 3;
            int actual = 0;

            var orders = dc.tblOrders;

            actual = orders.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }
        /*
        [TestMethod]
        public void LoadOrdersLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblOrder
            var orders = from dt in dc.tblOrders
                              select dt;

            actual = orders.Count();

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
            tblOrder newrow = new tblOrder();

            //set column values
            newrow.Id = -98;
            newrow.CustomerId = 1;
            newrow.UserId = 1;
            newrow.OrderDate = DateTime.Now;
            newrow.ShipDate = DateTime.Now;

            // Insert of the row
            dc.tblOrders.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }
        
        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblOrder where id = -99
            tblOrder existingOrder = (from dt in dc.tblOrders
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingOrder != null)
            {
                //update description
                existingOrder.CustomerId = 1;
                existingOrder.UserId = 1;
                existingOrder.OrderDate = DateTime.Now;
                existingOrder.ShipDate = DateTime.Now;
                dc.SaveChanges();
            }

            tblOrder updatedOrder = (from dt in dc.tblOrders
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingOrder.CustomerId, updatedOrder.CustomerId);

        }
        
        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblOrder where id = -99
            tblOrder existingOrder = (from dt in dc.tblOrders
                                                where dt.Id == -98
                                                select dt).FirstOrDefault();

            if (existingOrder != null)
            {
                //update description
                dc.tblOrders.Remove(existingOrder);
                dc.SaveChanges();
            }

            tblOrder deletedOrder = (from dt in dc.tblOrders
                                               where dt.Id == -98
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedOrder);

        }
    }
}
