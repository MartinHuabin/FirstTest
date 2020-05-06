using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Canada2DCode.Models;
using Canada2DCode.DataBaseEngine;
using System.IO;
using System.Drawing;
using ZXing;
using System.Web.Configuration;

namespace Canada2DCode.Controllers
{

    [Authorize]
    [RoutePrefix("api")]
    public class DataApiController : ApiController
    {



        string BaseUrlStr = WebConfigurationManager.AppSettings["BaseUrl"].ToString();


        //[Authorize]
        [HttpGet]
        [Route("customers")]
        public List<Customers> GetCustomers(HttpRequestMessage request)
        {
            List<Customers> customers = new List<Customers>();
            using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
            {
                
                var customersvalue = ctx.Customers.ToList();

                //return request.CreateResponse<CustomerModel[]>(HttpStatusCode.OK, customers.ToArray());
                return customersvalue;


            }
        }


        [HttpGet]
        [Route("customer/{customerId}")]
        public HttpResponseMessage GetCustomer(HttpRequestMessage request, int customerId)
        {
            //var customers = DataFactory.GetCustomers();
            //var customer = customers.FirstOrDefault(item => item.CustomerId == customerId);
            List<ObjectPersonModel> ObjectPersonList = new List<ObjectPersonModel>();
            List<ObjectTbl> _ObjectTbls = new List<ObjectTbl>();
            ObjectPerson _ObjectPerson = new ObjectPerson();
            ObjectPersonModel _ObjectPersonModel = new ObjectPersonModel();



            using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
            {

                _ObjectTbls = ctx.ObjectTbl.Where(t=>t.OrganizationID_FK == customerId).ToList<ObjectTbl>();


                if (_ObjectTbls != null && _ObjectTbls.Count != 0)

                {

                    foreach (ObjectTbl itemObjectTbl in _ObjectTbls)
                    {

                        _ObjectPerson = ctx.ObjectPerson.Where(t => t.ObjectID_FK == itemObjectTbl.Object_id && t.Deleted_Date == null).FirstOrDefault();

                        _ObjectPersonModel = new ObjectPersonModel();

                        _ObjectPersonModel.Object_id = itemObjectTbl.Object_id;
                        _ObjectPersonModel.Object_name = itemObjectTbl.Object_name;
                        _ObjectPersonModel.Category_id = itemObjectTbl.Category_id;
                        _ObjectPersonModel.Model_year = itemObjectTbl.Model_year;
                        _ObjectPersonModel.OrganizationID_FK = itemObjectTbl.OrganizationID_FK;
                        _ObjectPersonModel.C2DMACode = itemObjectTbl.C2DMACode;
                        _ObjectPersonModel.QRCode = itemObjectTbl.QRCode;
                        _ObjectPersonModel.ContentsURL = itemObjectTbl.ContentsURL;
                        _ObjectPersonModel.PrivateKey = itemObjectTbl.PrivateKey;
                        _ObjectPersonModel.OpenToRole = itemObjectTbl.OpenToRole;
                        _ObjectPersonModel.BlockID = itemObjectTbl.BlockID;
                        _ObjectPersonModel.NextChainID = itemObjectTbl.NextChainID;
                        _ObjectPersonModel.ObjectPersonData = _ObjectPerson;

                        ObjectPersonList.Add(_ObjectPersonModel);
                    }

                }

            }

            return request.CreateResponse<List<ObjectPersonModel>>(HttpStatusCode.OK, ObjectPersonList);
        }

        [HttpGet]
        [Route("products")]
        public HttpResponseMessage GetProducts(HttpRequestMessage request)
        {
            var products = DataFactory.GetProducts();

            return request.CreateResponse<ProductModel[]>(HttpStatusCode.OK, products.ToArray());
        }

        [HttpGet]
        [Route("product/{productId}")]
        public HttpResponseMessage GetProduct(HttpRequestMessage request, int productId)
        {
            var products = DataFactory.GetProducts();
            var product = products.FirstOrDefault(item => item.ProductId == productId);

            return request.CreateResponse<ProductModel>(HttpStatusCode.OK, product);
        }

        [HttpGet]
        [Route("orders")]
        public HttpResponseMessage GetOrders(HttpRequestMessage request)
        {
            var orders = DataFactory.GetOrders();

            return request.CreateResponse<OrderModel[]>(HttpStatusCode.OK, orders.ToArray());
        }

        [HttpGet]
        [Route("order/{orderId}")]
        public HttpResponseMessage GetOrder(HttpRequestMessage request, int orderId)
        {
            var orders = DataFactory.GetOrders();
            var order = orders.FirstOrDefault(item => item.OrderId == orderId);

            return request.CreateResponse<OrderModel>(HttpStatusCode.OK, order);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("order/detail/{ObjectId}/{PersonID}")]
        public HttpResponseMessage GetOrderDetail(HttpRequestMessage request, int ObjectId, int PersonID)
        {
            ObjectPersonModel _ObjectPersonModel = new ObjectPersonModel();

            using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
            {

                if (ObjectId > 0)
                {

                    ObjectTbl Objectvalue = ctx.ObjectTbl.Where(cu => cu.Object_id == ObjectId).FirstOrDefault();




                    _ObjectPersonModel.Object_id = Objectvalue.Object_id;
                    _ObjectPersonModel.Object_name = Objectvalue.Object_name;

                    _ObjectPersonModel.Category_id = Objectvalue.Category_id;
                    _ObjectPersonModel.Model_year = Objectvalue.Model_year;

                    _ObjectPersonModel.OrganizationID_FK = Objectvalue.OrganizationID_FK;
                    _ObjectPersonModel.C2DMACode = Objectvalue.C2DMACode;
                    _ObjectPersonModel.QRCode = Objectvalue.QRCode;
                    _ObjectPersonModel.ContentsURL = Objectvalue.ContentsURL;
                    _ObjectPersonModel.PrivateKey = Objectvalue.PrivateKey;
                    _ObjectPersonModel.OpenToRole = Objectvalue.OpenToRole;
                    _ObjectPersonModel.BlockID = Objectvalue.BlockID;
                    _ObjectPersonModel.NextChainID = Objectvalue.NextChainID;


                    ObjectPerson _ObjectPerson = ctx.ObjectPerson.Where(cu => cu.ObjectID_FK == ObjectId).FirstOrDefault();

                    if (_ObjectPerson == null)
                    {

                        _ObjectPerson = new ObjectPerson();
                    }
                    _ObjectPersonModel.ObjectPersonData = _ObjectPerson;

                }

            }
       

            return request.CreateResponse<ObjectPersonModel>(HttpStatusCode.OK, _ObjectPersonModel);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("customer")]
        public HttpResponseMessage PostCustomers(CustomerModel _CustomerModel)
        {
            Customers _customers = new Customers();
            List<Customers> customers = new List<Customers>();
            using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
            {

                _customers = new Customers();

                _customers.FirstName = _CustomerModel.FirstName;
                _customers.LastName = _CustomerModel.LastName;
                _customers.Email = _CustomerModel.Email;
                _customers.Password = _CustomerModel.Password;
                _customers.PasswordKey = _CustomerModel.ConfirmPassword;
                _customers.Conditions_BT = _CustomerModel.Conditions_BT;
                _customers.Url = _CustomerModel.Url;


                ctx.Customers.Add(_customers);

                ctx.SaveChanges();

                customers = ctx.Customers.ToList();

            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, _CustomerModel) ;
            //response.Content = new StringContent("hello", Encoding.Unicode);

            //HttpRequestMessage request = new HttpRequestMessage();
            //return Request.CreateResponse(HttpStatusCode.OK, customers);
            return response;
            //return 1;
        }

        [HttpGet]
        [Route("ReadProduct/{userid}")]
        public string ReadProduct(string userid)
        {
           
            Image img = null;

            if (string.IsNullOrEmpty(userid))
            {

                userid = "abcd100";
            }
            using (var ms = new MemoryStream())
            {
                var writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                writer.Options.Height = 180;
                writer.Options.Width = 180;
                writer.Options.PureBarcode = true;
                img = writer.Write(userid);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return System.Convert.ToBase64String(ms.ToArray());
            }

        }

        [HttpGet]
       [Route("ReadPerson")]
        public string ReadPerson(int Objectid, string Objectype)
        {

            Image img = null;

            ObjectPerson _ObjectPerson = new ObjectPerson();

            string BarcodeFormatstr = "BarcodeFormat.QR_CODE";

            BarcodeWriter writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.QR_CODE };

            if (string.IsNullOrEmpty(Objectype) || Objectype == "QR_Code")
            {

                using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
                {

                    _ObjectPerson = ctx.ObjectPerson.Where(t => t.ObjectID_FK == Objectid && t.Deleted_Date == null).FirstOrDefault();

                    if (_ObjectPerson != null)
                    {
                        BarcodeFormatstr = _ObjectPerson.PersonDetailsUrl;
                       
                    }
                    else
                    {

                        BarcodeFormatstr = BaseUrlStr + "/indexobject.html#/PersonDetail/" + Objectid.ToString();

                    }


                }

                   
            }
            using (var ms = new MemoryStream())
            {
                if (string.IsNullOrEmpty(Objectype) || Objectype == "QR_Code" || Objectype ==  "Data_Matrix")
                {

                    writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                }
                else if (Objectype == "Data_Matrix")
                {

                    writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.DATA_MATRIX };


                }
                
                writer.Options.Height = 180;
                writer.Options.Width = 180;
                writer.Options.PureBarcode = true;
                img = writer.Write(BarcodeFormatstr);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return System.Convert.ToBase64String(ms.ToArray());
            }

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("ScanQRCode")]
        public dynamic GetScanQRCode()
        {
            QRScannerModel _QRScannerModel = new QRScannerModel();
            string fileName = string.Empty;
            string costomerid = "";
            string scanqrcodeid = "";
            try
            {
                var barcodeReader = new BarcodeReader();
                if (!HttpContext.Current.Request.Files.AllKeys.Any()) {

                    return _QRScannerModel;
                }


                var requestfile = HttpContext.Current.Request;

                var file = requestfile.Files[0];

                costomerid = requestfile.Params["ScanQRCodeID"];
                scanqrcodeid = requestfile.Params["CustomerID"];


                if (file.ContentLength > 0)
                {
                    int fileSize = file.ContentLength;
                    //fileName = Path.GetFileName(file.FileName);

                    using(Stream fs = System.Web.HttpContext.Current.Request.Files[0].InputStream)
                    {
                        var barcodeBitmap = (Bitmap)Bitmap.FromStream(fs);
                    

                        // decode the barcode from the in memory bitmap
                        var barcodeResult = barcodeReader.Decode(barcodeBitmap);

                        if (barcodeResult != null)
                        {
                            _QRScannerModel.ScanType = barcodeResult.BarcodeFormat.ToString();
                            _QRScannerModel.ScanContent = barcodeResult.Text;

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                string errmessage = ex.Message;
            }
            

            return _QRScannerModel;
        }
    }
}
