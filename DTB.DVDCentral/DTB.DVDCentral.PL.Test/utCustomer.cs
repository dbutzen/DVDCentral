using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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
        public void LoadCustomersTest()
        {


            int expected = 3;
            int actual = 0;

            var customers = dc.tblCustomers;

            actual = customers.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }

        [TestMethod]
        public void LoadCustomersById()
        {
            tblCustomer existingCustomer = (from dt in dc.tblCustomers
                                            where dt.Id == 1
                                            select dt).FirstOrDefault();



            Assert.IsNotNull(existingCustomer);
        }
        

        [TestMethod]
        public void InsertTest()
        {
            tblCustomer newrow = new tblCustomer();

            newrow.Id = -98;
            newrow.FirstName = "FirstName";
            newrow.LastName = "LastName";
            newrow.Address = "Address";
            newrow.City = "City";
            newrow.Phone = "Phone";
            newrow.State = "ST";
            newrow.ZIP = 55555;
            newrow.UserId = 7;
            dc.tblCustomers.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }

        [TestMethod]
        public void UpdateTest()
        {

            
            tblCustomer existingCustomer = (from dt in dc.tblCustomers
                                            where dt.Id == 1
                                            select dt).FirstOrDefault();

            if (existingCustomer != null)
            {
                //update description
                existingCustomer.FirstName = "newFirstName";
                dc.SaveChanges();
            }

            tblCustomer updatedCustomer = (from dt in dc.tblCustomers
                                           where dt.Id == 1
                                           select dt).FirstOrDefault();

            Assert.AreEqual(existingCustomer.FirstName, updatedCustomer.FirstName);

        }
        
        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblCustomer where id = -99
            tblCustomer existingCustomer = (from dt in dc.tblCustomers
                                                where dt.Id == -98
                                                select dt).FirstOrDefault();

            if (existingCustomer != null)
            {
                //update description
                dc.tblCustomers.Remove(existingCustomer);
                dc.SaveChanges();
            }

            tblCustomer deletedCustomer = (from dt in dc.tblCustomers
                                               where dt.Id == -98
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedCustomer);

        }
    }
}