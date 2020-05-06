using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using System.IO;
using System.Drawing;
using Canada2DCode.Models;

namespace Canada2DCode.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customer()
        {
            ViewBag.Message = "Customer Section";

            return View();
        }

        public ActionResult Register() // Create Login Page
        {
            return View();
        }
        public ActionResult Product() // Create Login Page
        {
            return View();
        }
        public ActionResult ScanQRCode() // Create Login Page
        {
            return View();
        }
        public ActionResult Part2() // Fetch & Show Database Data
        {
            return View();
        }

        public ActionResult Part3() // Create Login Page
        {
            return View();
        }

        public ActionResult Part4() // Retrive & Display Tabuler Data
        {
            return View();
        }

        public ActionResult Part5() // Cascade Dropdown
        {
            return View();
        }

        public ActionResult Part6() // Simple registration with Validation
        {
            return View();
        }

        public ActionResult PersonDetails(int ObjectId, int PersonID) 
        //public ActionResult PersonDetails() // Nested Tabuler Data
        {
            return View();
            
        }

        public ActionResult Part8() // Upload File with Data
        {
            return View();
        }

        public ActionResult Part9() // Routing
        {
            return View();
        }

        public ActionResult ReadProduct(string userid)
        {




            Image img = null;

            if (string.IsNullOrEmpty(userid))
            {

                userid = "abcd100";
            }
            using (var ms = new MemoryStream())
            {
                var writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                writer.Options.Height = 280;
                writer.Options.Width = 280;
                writer.Options.PureBarcode = true;
                img = writer.Write(userid);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return File(ms.ToArray(), "image/jpeg");
            }

        }

    }
}
