using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
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
        public void LoadDirectorsTest()
        {


            int expected = 3;
            int actual = 0;

            var directors = dc.tblDirectors;

            actual = directors.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }
        /*
        [TestMethod]
        public void LoadDirectorsLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblDirector
            var directors = from dt in dc.tblDirectors
                              select dt;

            actual = directors.Count();

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
            tblDirector newrow = new tblDirector();

            //set column values
            newrow.Id = -98;
            newrow.FirstName = "FirstName";
            newrow.LastName = "LastName";

            // Insert of the row
            dc.tblDirectors.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }
        
        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblDirector where id = -99
            tblDirector existingDirector = (from dt in dc.tblDirectors
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingDirector != null)
            {
                //update description
                existingDirector.FirstName = "newFirstName";
                dc.SaveChanges();
            }

            tblDirector updatedDirector = (from dt in dc.tblDirectors
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingDirector.FirstName, updatedDirector.FirstName);

        }
        /*
        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblDirector where id = -99
            tblDirector existingDirector = (from dt in dc.tblDirectors
                                                where dt.Id == -98
                                                select dt).FirstOrDefault();

            if (existingDirector != null)
            {
                //update description
                dc.tblDirectors.Remove(existingDirector);
                dc.SaveChanges();
            }

            tblDirector deletedDirector = (from dt in dc.tblDirectors
                                               where dt.Id == -98
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedDirector);

        }*/
    }
}