using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace DTB.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadByIdTest()
        {
            
            OrderItem orderItem = OrderItemManager.LoadByOrderId(1);
            Assert.IsNotNull(orderItem);
        }
    }
}
