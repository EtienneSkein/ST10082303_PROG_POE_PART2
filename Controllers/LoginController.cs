using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ST10082303_PROG_POE_PART2.Controllers;

namespace ST10082303_PROG_POE_PART2.Controllers
{
    public class LoginController : Controller
    {
        public DatabaseManager databasemanager = new DatabaseManager();

        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult CheckCreds(FormCollection LoginForm)
        {
            string username = LoginForm["username"];
            string password = LoginForm["password"];
            string usertype = LoginForm["toggle"];

            if (usertype == null)
            {
                if (databasemanager.AuthenticateFarmer(username, password))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("AddProduct", "Farmer");
                }
                return Content("False");
            }
            else
            {
                if (databasemanager.AuthenticateEmployee(username, password))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("CreateFarmer", "Employee");
                }
                return Content("False");

            }
        }
    }
}
