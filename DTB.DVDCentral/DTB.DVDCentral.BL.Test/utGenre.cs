using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utGenre
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Genre> genres = new List<Genre>();
            genres = GenreManager.Load();
            int expected = 4;
            Assert.AreEqual(expected, genres.Count);
        }
        /*
        [TestMethod]
        public void InsertTest()
        {
            Genre genre = new Genre();
            genre.Description = "Movie Desc Insert";

            int result = GenreManager.Insert(genre, true);
            Assert.IsTrue(result > 0);
        }
        */
        [TestMethod]
        public void UpdateTest()
        {
            Genre genre = GenreManager.LoadById(3);
            genre.Description = "Movie Description";

            int result = GenreManager.Update(genre, true);
            Assert.IsTrue(result > 0);
        }
        /*

        [TestMethod]
        public void DeleteTest()
        {
            int results = GenreManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        */
    }
}
