using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pos2_wef.Models;

namespace Pos2_wef.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            User userModel = new Models.User();
            return View(userModel);
        }

        // POST: User
        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            using(DBModels dbModel = new DBModels())
            {
                if(dbModel.Users.Any(x => x.Username == userModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exists.";
                    return View("AddOrEdit", userModel);
                } 
                
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();

            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddOrEdit", new User());
        }
    }
}