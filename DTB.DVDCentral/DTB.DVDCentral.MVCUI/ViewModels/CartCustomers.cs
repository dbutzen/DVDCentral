using DTB.DVDCentral.BL.Models;
using DTB.DVDCentral.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTB.DVDCentral.MVCUI.ViewModels
{
    public class CartCustomers
    {
        public ShoppingCart cart { get; set; }
        public List<Customer> customers { get; set; }
        public int custID { get; set; }
        
    }
}