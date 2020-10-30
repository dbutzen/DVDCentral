using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
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
            directors = DirectorManager.Load();
            return View(directors);
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
            Director director = new Director();
            return View(director);
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
            Director director = new Director();
            director = DirectorManager.LoadById(id);
            return View(director);
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
            Director director = new Director();
            director = DirectorManager.LoadById(id);
            return View(director);
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
