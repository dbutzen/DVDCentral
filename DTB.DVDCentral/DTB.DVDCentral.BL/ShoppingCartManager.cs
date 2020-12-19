using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.BL
{
    public static class ShoppingCartManager
    {
        public static void Checkout(ShoppingCart cart)
        {
            /* For DVD Central, do these things when you checkout
             * Add OrderManager
             * 1) Insert a tblOrder. Get the Order.Id
             * 2)Loop through the ITems, and insert a tblOrderITem record
             * with the new Order.Id
             * 3) Remove the items from the cart
             */

            Order order = new Order();
            order.CustomerId = 1;
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.Now.AddDays(3);
            order.UserId = cart.UserId;
            order.CustomerId = cart.CustomerId;
            OrderManager.Insert(order, cart.Items);
            cart.CheckOut();
        }
        public static void Add(ShoppingCart cart, Movie movie)
        {
            cart.Add(movie);
        }
        public static void Remove(ShoppingCart cart, Movie movie)
        {
            cart.Remove(movie);
        }

    }
}
