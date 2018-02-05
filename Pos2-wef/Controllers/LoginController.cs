using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pos2_wef.Models;

namespace Pos2_wef.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            using(Entities db = new Entities())
            {
                var userDetails = db.Users.Where
                    (x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Incorrect username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
            
        }

        public ActionResult Logout()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}