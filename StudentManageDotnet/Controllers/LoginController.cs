using StudentManageDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManageDotnet.Controllers
{
    public class LoginController : Controller
    {
        StudentManagedotnetEntities db = new StudentManagedotnetEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User obj)
        {
            
                //if (ModelState.IsValid)

                //{
                    //using (StudentManagedotnetEntities db = new StudentManagedotnetEntities()) 
                    //{
                        
                  //  }
                    
                //}
            var res = db.Users.Where(x => x.username.Equals(obj.username) && x.password.Equals(obj.password)).FirstOrDefault();
            if (res != null)
            {
                Session["UserID"] = res.id.ToString();
                Session["UserName"] = res.username.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "The Username or password is Invalid");
            }

            return View(obj);
        }

        public ActionResult Logout() 
        {
            Session.Clear();
            return RedirectToAction("Index","Login");
        }
        
    }
}