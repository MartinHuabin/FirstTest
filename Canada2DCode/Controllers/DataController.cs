using Canada2DCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Canada2DCode.DataBaseEngine;

namespace Canada2DCode.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/
        //For fetch Last Contact
        public JsonResult GetLastContact()
        {
            Contacts c = null;
            //here Canada2DCodeEntities our DBContext
            using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
            {
                c = dc.Contacts.OrderByDescending(a => a.ContactID).Take(1).FirstOrDefault();
            }
            return new JsonResult { Data = c, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UserLogin(LoginData d)
        {
            using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
            {
                var user = dc.Users.Where(a => a.Username.Equals(d.Username) && a.Password.Equals(d.Password)).FirstOrDefault();
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetEmployeeList()
        {
            List<Employees> Employees = new List<Employees>();
            using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
            {
                Employees = dc.Employees.OrderBy(a => a.FirstName).ToList();
            }

            return new JsonResult { Data = Employees, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // Fetch Country
        public JsonResult GetCountries()
        {
            List<Country> allCountry = new List<Country>();
            using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
            {
                allCountry = dc.Country.OrderBy(a => a.CountryName).ToList();
            }
            return new JsonResult { Data = allCountry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // Fetch State by Country ID
        public JsonResult GetStates(int countryID)
        {
            List<State> allState = new List<State>();
            using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
            {
                allState = dc.State.Where(a => a.CountryID.Equals(countryID)).OrderBy(a => a.StateName).ToList();
            }
            return new JsonResult { Data = allState, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Save Simple Registration Form
        [HttpPost]
        public JsonResult Register(Users u)
        {
            string message = "";
            //Here we will save data to the database
            if (ModelState.IsValid)
            {
                using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
                {
                    //check username available
                    var user = dc.Users.Where(a => a.Username.Equals(u.Username)).FirstOrDefault();
                    if (user == null)
                    {
                        //Save here
                        dc.Users.Add(u);
                        dc.SaveChanges();
                        message = "Success";
                    }
                    else
                    {
                        message = "Username not available!";
                    }
                }
            }
            else
            {
                message = "Failed!";
            }
            return new JsonResult { Data = message };
        }

        [HttpGet]
        public JsonResult CustomerOrders()
        {
            List<CustomerOrders> CO = new List<CustomerOrders>();
            using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
            {
                var cust = dc.Customers.OrderBy(a => a.CustomerId).ToList();
                foreach (var i in cust)
                {
                    var orders = dc.Orders.Where(a => a.CustomerID.Equals(i.CustomerId)).OrderBy(a => a.OrderDate).ToList();
                    CO.Add(new CustomerOrders
                    {
                        Customer = i,
                        Orders = orders
                    });
                }
            }
            return new JsonResult { Data = CO, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveFiles(string description)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;

                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), fileName));

                    UploadedFiles f = new UploadedFiles
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                        Description = description,
                        FileSize = size
                    };
                    using (Canada2DCodeEntities dc = new Canada2DCodeEntities())
                    {
                        dc.UploadedFiles.Add(f);
                        dc.SaveChanges();
                        Message = "File uploaded successfully";
                        flag = true;
                    }
                }
                catch (Exception)
                {
                    Message = "File upload failed! Please try again";
                }

            }
            return new JsonResult { Data = new { Message = Message, Status = flag } };
        }
    }
}
