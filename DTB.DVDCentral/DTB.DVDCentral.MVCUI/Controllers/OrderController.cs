using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using DTB.DVDCentral.MVCUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class OrderController : Controller
    {
        List<Order> orders;
        // GET: Order
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                orders = OrderManager.Load();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            Order order = new Order();
            order = OrderManager.LoadById(id);
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Order order = new Order();
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
                // TODO: Add insert logic here
                OrderManager.Insert(order);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Order order = new Order();
                order = OrderManager.LoadById(id);
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                // TODO: Add update logic here
                OrderManager.Update(order);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Order order = new Order();
                order = OrderManager.LoadById(id);
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Order order)
        {
            try
            {
                // TODO: Add delete logic here
                OrderManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}