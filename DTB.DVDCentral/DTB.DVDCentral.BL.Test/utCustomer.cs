using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Customer> customers = new List<Customer>();
            customers = CustomerManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, customers.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Customer customer = new Customer();
            customer.FirstName = "FirstName";
            customer.LastName = "LastName";
            customer.Phone = "Phone";
            customer.State = "St";
            customer.Address = "Address";
            customer.City = "City";
            customer.ZIP = 55555;
            customer.UserId = 1234512345;

            int result = CustomerManager.Insert(customer, true);
            Assert.IsTrue(result > 0);
        }
        
        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(3);
            customer.FirstName = "New First Name";

            int result = CustomerManager.Update(customer, true);
            Assert.AreEqual("New First Name", customer.FirstName);
        }
        
        

        [TestMethod]
        public void DeleteTest()
        {
            int results = CustomerManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        
    }
}