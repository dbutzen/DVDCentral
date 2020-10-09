using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utDirector
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Director> directors = new List<Director>();
            directors = DirectorManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, directors.Count);
        }
        
        [TestMethod]
        public void InsertTest()
        {
            Director director = new Director();
            director.FirstName = "FirstName";
            director.LastName = "LastName";

            int result = DirectorManager.Insert(director, true);
            Assert.IsTrue(result > 0);
        }
        /*
        [TestMethod]
        public void UpdateTest()
        {
            Director director = DirectorManager.LoadById(3);
            director.Description = "Movie Description";

            int result = DirectorManager.Update(director, true);
            Assert.IsTrue(result > 0);
        }
        */
        /*

        [TestMethod]
        public void DeleteTest()
        {
            int results = DirectorManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        */
    }
}