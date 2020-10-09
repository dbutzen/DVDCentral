using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DTB.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
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
        public void LoadMoviesTest()
        {


            int expected = 3;
            int actual = 0;

            var movies = dc.tblMovies;

            actual = movies.Count();

            Assert.AreEqual(expected, actual);

            dc = null;

        }

        /*[TestMethod]
        public void LoadMoviesLINQTest()
        {
            //Instantiate a datacontext variable (pipe) connected to the database


            //What I expect to get back
            int expected = 4;
            int actual = 0;

            //Retrieve degree types from DB
            //Select * from tblMovie
            var movies = from dt in dc.tblMovies
                         select dt;

            actual = movies.Count();

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
            tblMovie newrow = new tblMovie();

            //set column values
            newrow.Id = -98;
            newrow.Description = "My New Movie";
            newrow.Title = "New Movie";
            newrow.Cost = (decimal)20.20;
            newrow.DirectorId = 1;
            newrow.FormatId = 1;
            newrow.ImagePath = "Q";
            newrow.InStkQty = 1;
            newrow.RatingId = 1;


            // Insert of the row
            dc.tblMovies.Add(newrow);

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
            // select * from tblMovie where id = -99
            tblMovie existingMovie = (from dt in dc.tblMovies
                                      where dt.Id == 1
                                      select dt).FirstOrDefault();

            if (existingMovie != null)
            {
                //update description
                existingMovie.Description = "Test";
                dc.SaveChanges();
            }

            tblMovie updatedMovie = (from dt in dc.tblMovies
                                     where dt.Id == 1
                                     select dt).FirstOrDefault();

            Assert.AreEqual(existingMovie.Description, updatedMovie.Description);

        }
        
        [TestMethod]
        public void DeleteTest()
        {

            // retrieve one degreetype
            // select * from tblMovie where id = -99
            tblMovie existingMovie = (from dt in dc.tblMovies
                                                where dt.Id == -98
                                                select dt).FirstOrDefault();

            if (existingMovie != null)
            {
                //update description
                dc.tblMovies.Remove(existingMovie);
                dc.SaveChanges();
            }

            tblMovie deletedMovie = (from dt in dc.tblMovies
                                               where dt.Id == -98
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedMovie);
        
        }*/
    }
}
