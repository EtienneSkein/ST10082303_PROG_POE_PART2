using ST10082303_PROG_POE_PART2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST10082303_PROG_POE_PART2.Controllers
{
    public class EmployeeController : Controller
    {
        DatabaseManager databasemanager = new DatabaseManager();

        // GET: Employee
        public ActionResult CreateFarmer(FormCollection input)
        {
            Farmer temp = new Farmer();
            temp.Username = input["username"];
            temp.Password = input["password"];
            temp.JoinDate = Convert.ToDateTime(input["joindate"]).Date;
            databasemanager.AddFarmer(temp);
            return View();
        }
    }
}