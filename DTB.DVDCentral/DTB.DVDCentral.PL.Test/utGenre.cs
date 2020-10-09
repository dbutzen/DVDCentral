using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre
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
        public void LoadGenresTest()
        {


            int expected = 4;
            int actual = 0;

            var genres = dc.tblGenres;

            actual = genres.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }
        /*
        [TestMethod]
        public void LoadGenresLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblGenre
            var genres = from dt in dc.tblGenres
                              select dt;

            actual = genres.Count();

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
            tblGenre newrow = new tblGenre();

            //set column values
            newrow.Id = -98;
            newrow.Description = "My New Genre";

            // Insert of the row
            dc.tblGenres.Add(newrow);

            //commit the changes (insert a row)
            // then return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);



        }
        /*
        [TestMethod]
        public void UpdateTest()
        {

            // retrieve one degreetype
            // select * from tblGenre where id = -99
            tblGenre existingGenre = (from dt in dc.tblGenres
                                                where dt.Id == 1
                                                select dt).FirstOrDefault();

            if (existingGenre != null)
            {
                //update description
                existingGenre.Description = "Test";
                dc.SaveChanges();
            }

            tblGenre updatedGenre = (from dt in dc.tblGenres
                                               where dt.Id == 1
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingGenre.Description, updatedGenre.Description);

        }*/
        /*
        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblGenre where id = -99
            tblGenre existingGenre = (from dt in dc.tblGenres
                                                where dt.Id == -98
                                                select dt).FirstOrDefault();

            if (existingGenre != null)
            {
                //update description
                dc.tblGenres.Remove(existingGenre);
                dc.SaveChanges();
            }

            tblGenre deletedGenre = (from dt in dc.tblGenres
                                               where dt.Id == -98
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedGenre);

        }*/
    }
}
