using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class FormatController : Controller
    {
        List<Format> formats;
        // GET: Format
        public ActionResult Index()
        {
            formats = FormatManager.Load();
            return View(formats);
        }

        // GET: Format/Details/5
        public ActionResult Details(int id)
        {
            Format format = new Format();
            format = FormatManager.LoadById(id);
            return View(format);
        }

        // GET: Format/Create
        public ActionResult Create()
        {
            Format format = new Format();
            return View(format);
        }

        // POST: Format/Create
        [HttpPost]
        public ActionResult Create(Format format)
        {
            try
            {
                // TODO: Add insert logic here
                FormatManager.Insert(format);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Format/Edit/5
        public ActionResult Edit(int id)
        {
            Format format = new Format();
            format = FormatManager.LoadById(id);
            return View(format);
        }

        // POST: Format/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Format format)
        {
            try
            {
                // TODO: Add update logic here
                FormatManager.Update(format);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Format/Delete/5
        public ActionResult Delete(int id)
        {
            Format format = new Format();
            format = FormatManager.LoadById(id);
            return View(format);
        }

        // POST: Format/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Format format)
        {
            try
            {
                // TODO: Add delete logic here
                FormatManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
