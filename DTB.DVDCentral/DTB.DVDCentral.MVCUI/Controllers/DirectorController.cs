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
    public class DirectorController : Controller
    {
        List<Director> directors;
        // GET: Director
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                directors = DirectorManager.Load();
                return View(directors);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Director/Details/5
        public ActionResult Details(int id)
        {
            Director director = new Director();
            director = DirectorManager.LoadById(id);
            return View(director);
        }

        // GET: Director/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Director director = new Director();
                return View(director);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Director/Create
        [HttpPost]
        public ActionResult Create(Director director)
        {
            try
            {
                // TODO: Add insert logic here
                DirectorManager.Insert(director);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Director/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Director director = new Director();
                director = DirectorManager.LoadById(id);
                return View(director);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Director/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Director director)
        {
            try
            {
                // TODO: Add update logic here
                DirectorManager.Update(director);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Director/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Director director = new Director();
                director = DirectorManager.LoadById(id);
                return View(director);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Director/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Director director)
        {
            try
            {
                // TODO: Add delete logic here
                DirectorManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
