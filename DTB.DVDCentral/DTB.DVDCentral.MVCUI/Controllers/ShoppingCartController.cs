using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ShoppingCart cart;
        // GET: ShoppingCart
        public ActionResult Index()
        {
            GetShoppingCart();
            return View(cart);
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
    }
}