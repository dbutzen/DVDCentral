using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Format> formats = new List<Format>();
            formats = FormatManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, formats.Count);
        }
        
        [TestMethod]
        public void InsertTest()
        {
            Format format = new Format();
            format.Description = "Format Desc Insert";

            int result = FormatManager.Insert(format, true);
            Assert.IsTrue(result > 0);
        }
        /*
        [TestMethod]
        public void UpdateTest()
        {
            Format format = FormatManager.LoadById(3);
            format.Description = "Movie Description";

            int result = FormatManager.Update(format, true);
            Assert.IsTrue(result > 0);
        }*/
        /*

        [TestMethod]
        public void DeleteTest()
        {
            int results = FormatManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        */
    }
}