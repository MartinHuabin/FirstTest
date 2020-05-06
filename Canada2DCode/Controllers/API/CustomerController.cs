using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Canada2DCode.Models;
using Canada2DCode.DataBaseEngine;
using System.Data.Entity.Validation;

namespace Canada2DCode.Controllers.API
{

    [Authorize]
    [RoutePrefix("api/CustomerData")]
    public class CustomerDataController : ApiController
    {

        [HttpGet] 
        [Route("GetCustomersbyId")]
        public CustomerModel GetCustomersbyId(int customerid)
        {
            CustomerModel customers = new CustomerModel();
            using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
            {

                Customers customersvalue = ctx.Customers.Where(cu => cu.CustomerId == customerid).FirstOrDefault();

                customers.CustomerId = customersvalue.CustomerId;
                customers.FirstName = customersvalue.FirstName;
                customers.LastName = customersvalue.LastName;
                customers.Email = customersvalue.Email;
                customers.Password = customersvalue.Password;
                customers.ConfirmPassword = customersvalue.PasswordKey;
                customers.Conditions_BT = (bool)customersvalue.Conditions_BT;
                customers.Url = customersvalue.Url;
                customers.AddID_FK = customersvalue.AddID_FK;
                customers.Telephone = customersvalue.Telephone;

                List<Addresses> AddressesList = ctx.Addresses.Where(cu => cu.OrganizationID_FK == customerid).ToList();

                if (AddressesList == null) {

                    AddressesList = new List<Addresses>();
                }
                customers.AddressesList = AddressesList;

                return customers;
            }
        }


        

        [HttpPost]
        [Route("SaveCustomers")]
        public HttpResponseMessage SaveCustomers(CustomerModel CustomerModelData)
        {
            Customers _customers = new Customers();

           Addresses _Addresses = new Addresses();

            List<Customers> customers = new List<Customers>();

            try
            {

                using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
                {

                    if (CustomerModelData.CustomerId > 0)

                    {

                        _customers = ctx.Customers.Where(t => t.CustomerId == CustomerModelData.CustomerId).FirstOrDefault();
                        _customers.FirstName = CustomerModelData.FirstName;
                        _customers.LastName = CustomerModelData.LastName;
                        _customers.Email = CustomerModelData.Email;
                        _customers.Password = CustomerModelData.Password;
                        _customers.PasswordKey = CustomerModelData.ConfirmPassword;
                        _customers.Conditions_BT = CustomerModelData.Conditions_BT;
                        _customers.Url = CustomerModelData.Url;

                        ctx.SaveChanges();

                    }

                    else
                    {

                        _customers = new Customers();

                        _customers.FirstName = CustomerModelData.FirstName;
                        _customers.LastName = CustomerModelData.LastName;
                        _customers.Email = CustomerModelData.Email;
                        _customers.Password = CustomerModelData.Password;
                        _customers.PasswordKey = CustomerModelData.ConfirmPassword;
                        _customers.Conditions_BT = CustomerModelData.Conditions_BT;
                        _customers.Url = CustomerModelData.Url;

                        ctx.Customers.Add(_customers);
                        ctx.SaveChanges();
                    }

                    if (CustomerModelData.AddressesList != null && CustomerModelData.AddressesList.Count > 0)
                    {


                        if (CustomerModelData.AddressesList[0].AddID_PK > 0)
                        {

                            int addid = CustomerModelData.AddressesList[0].AddID_PK;

                            _Addresses = ctx.Addresses.Where(t => t.AddID_PK == addid).FirstOrDefault();


                            _Addresses.Address3 = "";
                            _Addresses.Note = "";
                            _Addresses.IsInActive = false;


                            _Addresses.Address1 = CustomerModelData.AddressesList[0].Address1;
                            _Addresses.Address2 = CustomerModelData.AddressesList[0].Address2;
                            _Addresses.City = CustomerModelData.AddressesList[0].City;
                            _Addresses.State = CustomerModelData.AddressesList[0].State;
                            _Addresses.Country = CustomerModelData.AddressesList[0].Country;
                            _Addresses.PostalCode = CustomerModelData.AddressesList[0].PostalCode;


                            _Addresses.OrganizationID_FK = CustomerModelData.CustomerId;


                            ctx.SaveChanges();
                        }
                        else
                        {

                            _Addresses = new Addresses();


                            _Addresses.Address3 = "";
                            _Addresses.Note = "";
                            _Addresses.IsInActive = false;


                            _Addresses.Address1 = CustomerModelData.AddressesList[0].Address1;
                            _Addresses.Address2 = CustomerModelData.AddressesList[0].Address2;
                            _Addresses.City = CustomerModelData.AddressesList[0].City;
                            _Addresses.State = CustomerModelData.AddressesList[0].State;
                            _Addresses.Country = CustomerModelData.AddressesList[0].Country;
                            _Addresses.PostalCode = CustomerModelData.AddressesList[0].PostalCode;


                            _Addresses.OrganizationID_FK = CustomerModelData.CustomerId;



                            ctx.Addresses.Add(_Addresses);
                            ctx.SaveChanges();
                        }




                    }


                    customers = ctx.Customers.ToList();

                }
            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;

            }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, CustomerModelData);
    
            return response;
   
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}