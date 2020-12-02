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
    public class CustomerController : Controller
    {
        List<Customer> customers;
        // GET: Customer
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                customers = CustomerManager.Load();
                return View(customers);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            Customer customer = new Customer();
            customer = CustomerManager.LoadById(id);
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Customer customer = new Customer();
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                CustomerManager.Insert(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Customer customer = new Customer();
                customer = CustomerManager.LoadById(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                CustomerManager.Update(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Customer customer = new Customer();
                customer = CustomerManager.LoadById(id);
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                // TODO: Add delete logic here
                CustomerManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
