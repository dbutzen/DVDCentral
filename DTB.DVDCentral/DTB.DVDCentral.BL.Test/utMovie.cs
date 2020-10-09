using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        /*
        [TestMethod]
        public void LoadTest()
        {
            List<Movie> movies = new List<Movie>();
            movies = MovieManager.Load();
            int expected = 4;
            Assert.AreEqual(expected, movies.Count);
        }
        */
        [TestMethod]
        public void LoadByID()
        {
            Movie movie = new Movie();
            movie = MovieManager.LoadById(1);
            Assert.IsNotNull(movie.Id);
        }
        /*
        [TestMethod]
        public void InsertTest()
        {
            Movie movie = new Movie();
            movie.Description = "Movie Desc Insert";

            int result = MovieManager.Insert(movie, true);
            Assert.IsTrue(result > 0);
        }
        */
        /*
        [TestMethod]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadById(3);
            movie.Description = "Movie Description";

            int result = MovieManager.Update(movie, true);
            Assert.IsTrue(result > 0);
        }
        */

        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        
    }
}