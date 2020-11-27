using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTB.DVDCentral.MVCUI.ViewModels
{
    public class CustomerOrders
    {
        public BL.Models.Customer Customer { get; set; }
        List<BL.Models.Order> Orders { get; set; }
    }
}