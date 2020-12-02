using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user, string returnurl)
        {
            try
            {
                if (UserManager.Login(user))
                {
                    Session["user"] = user;
                    return Redirect(returnurl);
                }
                else
                {
                    ViewBag.Message = "WRONG";
                    return View(user);
                }
            }catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return View();
        }
        public ActionResult Seed(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpPost]
        public ActionResult Seed(User user, string returnurl)
        {
            try
            {
                UserManager.Insert(user);
                Session["user"] = user;
                return Redirect(returnurl);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }
    }
}