using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utRating
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Rating> ratings = new List<Rating>();
            ratings = RatingManager.Load();
            int expected = 4;
            Assert.AreEqual(expected, ratings.Count);
        }
        
        [TestMethod]
        public void InsertTest()
        {
            Rating rating = new Rating();
            rating.Description = "Rating Desc Insert";

            int result = RatingManager.Insert(rating, true);
            Assert.IsTrue(result > 0);
        }
        
        [TestMethod]
        public void UpdateTest()
        {
            Rating rating = RatingManager.LoadById(3);
            rating.Description = "Rating Description";

            int result = RatingManager.Update(rating, true);
            Assert.IsTrue(result > 0);
        }
        

        [TestMethod]
        public void DeleteTest()
        {
            int results = RatingManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        
    }
}