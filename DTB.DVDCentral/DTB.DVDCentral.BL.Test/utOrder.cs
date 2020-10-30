using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Order> orders = new List<Order>();
            orders = OrderManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, orders.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Order order = new Order();
            OrderItem orderItem = new OrderItem();
            orderItem.Cost = 1;
            orderItem.Quantity = 1;
            orderItem.MovieId = 1;
            orderItem.OrderId = order.Id;
            order.CustomerId = 1;
            order.OrderDate = DateTime.Now;
            order.UserId = 1;
            order.ShipDate = DateTime.Now;


            int result = OrderManager.Insert(order, true);
            int result2 = OrderItemManager.Insert(orderItem, true);
            Assert.IsTrue(result > 0 && result2 > 0);
        }
        
        [TestMethod]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(3);
            order.CustomerId = 1;
            order.OrderDate = DateTime.Now;
            order.UserId = 1;
            order.ShipDate = DateTime.Now;

            int result = OrderManager.Update(order, true);
            Assert.IsTrue(result > 0);
        }
        
        

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderManager.Delete(3, true);
            Assert.IsTrue(results > 0);
        }
        
    }
}