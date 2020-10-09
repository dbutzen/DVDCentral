using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating
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
        public void LoadRatingsTest()
        {


            int expected = 4;
            int actual = 0;

            var ratings = dc.tblRatings;

            actual = ratings.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }
        /*
        [TestMethod]
        public void LoadRatingsLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblRating
            var ratings = from dt in dc.tblRatings
                              select dt;

            actual = ratings.Count();

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
            tblRating newrow = new tblRating();

            //set column values
            newrow.Id = -98;
            newrow.Description = "My New Rating";

            // Insert of the row
            dc.tblRatings.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }
        
        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblRating where id = -99
            tblRating existingRating = (from dt in dc.tblRatings
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingRating != null)
            {
                //update description
                existingRating.Description = "Test";
                dc.SaveChanges();
            }

            tblRating updatedRating = (from dt in dc.tblRatings
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingRating.Description, updatedRating.Description);

        }
        
        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblRating where id = -99
            tblRating existingRating = (from dt in dc.tblRatings
                                                where dt.Id == -98
                                                select dt).FirstOrDefault();

            if (existingRating != null)
            {
                //update description
                dc.tblRatings.Remove(existingRating);
                dc.SaveChanges();
            }

            tblRating deletedRating = (from dt in dc.tblRatings
                                               where dt.Id == -98
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedRating);

        }
    }
}