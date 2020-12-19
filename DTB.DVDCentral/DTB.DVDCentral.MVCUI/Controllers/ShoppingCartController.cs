using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using DTB.DVDCentral.MVCUI.Models;
using DTB.DVDCentral.MVCUI.ViewModels;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ShoppingCart cart;
        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                CartCustomers cc = new CartCustomers();
                cc.customers = CustomerManager.Load();
                cc.cart = cart;
                User user = (User)Session["user"];
                int validated = 0;
                foreach (Customer c in cc.customers)
                {
                    if (c.UserId == user.Id)
                    {
                        validated++;
                    }
                    
                }
                if (validated >= 1)
                {
                    //Good
                    GetShoppingCart();
                    return View(cart);
                }
                else
                {
                    return RedirectToAction("AssignCustomer", "ShoppingCart", new { returnurl = HttpContext.Request.Url });
                }


            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        //Show cart in sidebar - child action only for partial views
        [ChildActionOnly]
        public ActionResult CartDisplay()
        {
            GetShoppingCart();
            return PartialView(cart);
        }
        

        public ActionResult RemoveFromCart(int id)
        {
            GetShoppingCart();
            Movie movie = cart.Items.FirstOrDefault(i => i.Id == id); //lambda expression
            ShoppingCartManager.Remove(cart, movie);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            GetShoppingCart();
            Movie movie = MovieManager.LoadById(id);
            ShoppingCartManager.Add(cart, movie);
            Session["cart"] = cart;
            return RedirectToAction("Index", "Movie");
        }

        private void GetShoppingCart()
        {
            if (Session["cart"] == null)
                cart = new ShoppingCart();
            else
                cart = (ShoppingCart)Session["cart"];
        }
        public ActionResult Checkout()
        {
            GetShoppingCart();
            ShoppingCartManager.Checkout(cart);
            return View();
        }

        public ActionResult AssignCustomer()
        {
            CartCustomers cc = new CartCustomers();
            cc.customers = CustomerManager.Load();
            return View(cc);
        }

        [HttpPost]
        public ActionResult AssignCustomer(CartCustomers cc)
        {
            try
            {
                User user = (User)Session["user"];
                GetShoppingCart();
                cart.CustomerId = cc.custID;
                cart.UserId = user.Id;

                ShoppingCartManager.Checkout(cart);
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}