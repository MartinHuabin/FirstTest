using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Canada2DCode.Models;
using Canada2DCode.DataBaseEngine;
using System.Data.Entity.Validation;
using System.Web.Configuration;

namespace Canada2DCode.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/ObjectPerson")]
    public class ObjectPersonController : ApiController
    {


        string BaseUrlStr = WebConfigurationManager.AppSettings["BaseUrl"].ToString();

        private string ContentUrlstr = "home/Product#/codeperson/";

        private string PersonDetailsUrlstr = "indexobject.html#/PersonDetail/";


        [HttpGet]
        [Route("GetObjectPersonById")]
        public ObjectPersonModel GetObjectPersonById(int ObjectId, int PersonID)
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
            return _ObjectPersonModel;
        }

        [HttpPost]
        [Route("SaveObjectPerson")]
        public HttpResponseMessage SaveObjectPerson(ObjectPersonModel ObjectPersonModelData)
        {


            ObjectPersonModel _ObjectPersonModel = new ObjectPersonModel();

            ObjectTbl _ObjectTbl = new ObjectTbl();

            ObjectPerson _ObjectPerson = new ObjectPerson();


            try
            {

                using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
                {

                    if (ObjectPersonModelData.Object_id > 0)

                    {


                        _ObjectTbl = ctx.ObjectTbl.Where(t => t.Object_id == ObjectPersonModelData.Object_id).FirstOrDefault();

                        _ObjectTbl.Object_name = ObjectPersonModelData.Object_name;
                        _ObjectTbl.Category_id = ObjectPersonModelData.Category_id;
                        _ObjectTbl.Model_year = ObjectPersonModelData.Model_year;
                        _ObjectTbl.OrganizationID_FK = ObjectPersonModelData.OrganizationID_FK;
                        _ObjectTbl.C2DMACode = ObjectPersonModelData.C2DMACode;
                        _ObjectTbl.QRCode = ObjectPersonModelData.QRCode;
                        _ObjectTbl.ContentsURL = ObjectPersonModelData.ContentsURL;
                        _ObjectTbl.PrivateKey = ObjectPersonModelData.PrivateKey;
                        _ObjectTbl.OpenToRole = ObjectPersonModelData.OpenToRole;
                        _ObjectTbl.BlockID = ObjectPersonModelData.BlockID;
                        _ObjectTbl.NextChainID = ObjectPersonModelData.NextChainID;

                        ctx.SaveChanges();


                    }
                    else
                    {


                        _ObjectTbl = new ObjectTbl();

                        _ObjectTbl.Object_name = ObjectPersonModelData.Object_name;
                        _ObjectTbl.Category_id = ObjectPersonModelData.Category_id;
                        _ObjectTbl.Model_year = ObjectPersonModelData.Model_year;
                        _ObjectTbl.OrganizationID_FK = ObjectPersonModelData.OrganizationID_FK;
                        _ObjectTbl.C2DMACode = ObjectPersonModelData.C2DMACode;
                        _ObjectTbl.QRCode = ObjectPersonModelData.QRCode;
                        _ObjectTbl.ContentsURL = ObjectPersonModelData.ContentsURL;
                        _ObjectTbl.PrivateKey = ObjectPersonModelData.PrivateKey;
                        _ObjectTbl.OpenToRole = ObjectPersonModelData.OpenToRole;
                        _ObjectTbl.BlockID = ObjectPersonModelData.BlockID;
                        _ObjectTbl.NextChainID = ObjectPersonModelData.NextChainID;

                        ctx.ObjectTbl.Add(_ObjectTbl);
                        ctx.SaveChanges();


                    }
                    _ObjectTbl.ContentsURL = BaseUrlStr + ContentUrlstr + _ObjectTbl.Object_id.ToString();

                    if (ObjectPersonModelData.ObjectPersonData.PersonID_PK > 0)

                    {

                        _ObjectPerson = ctx.ObjectPerson.Where(t => t.PersonID_PK == ObjectPersonModelData.ObjectPersonData.PersonID_PK).FirstOrDefault();

                        _ObjectPerson.ObjectID_FK = ObjectPersonModelData.ObjectPersonData.ObjectID_FK;
                        _ObjectPerson.Person_Name = ObjectPersonModelData.ObjectPersonData.Person_Name;
                        _ObjectPerson.PersonType = ObjectPersonModelData.ObjectPersonData.PersonType;
                        _ObjectPerson.Description = ObjectPersonModelData.ObjectPersonData.Description;
                        _ObjectPerson.Gender = ObjectPersonModelData.ObjectPersonData.Gender;
                        _ObjectPerson.Title = ObjectPersonModelData.ObjectPersonData.Title;
                        _ObjectPerson.Telephone = ObjectPersonModelData.ObjectPersonData.Telephone;
                        if (ObjectPersonModelData.ObjectPersonData.Birthday != null)
                        {

                            _ObjectPerson.Birthday = ObjectPersonModelData.ObjectPersonData.Birthday;
                        }

                        else
                        {
                            _ObjectPerson.Birthday = DateTime.Today;
                        }
                        _ObjectPerson.Position = ObjectPersonModelData.ObjectPersonData.Position;
                        _ObjectPerson.Email = ObjectPersonModelData.ObjectPersonData.Email;
                        _ObjectPerson.AddID_FK = ObjectPersonModelData.ObjectPersonData.AddID_FK;
                        _ObjectPerson.C2DCodeType = ObjectPersonModelData.ObjectPersonData.C2DCodeType;
                        _ObjectPerson.PersonDetailsUrl = BaseUrlStr + PersonDetailsUrlstr + _ObjectPerson.ObjectID_FK.ToString();


                        ctx.SaveChanges();


                    }
                    else
                    {

                        try
                        {
                            _ObjectPerson = new ObjectPerson();

                            _ObjectPerson.ObjectID_FK = _ObjectTbl.Object_id;
                            _ObjectPerson.Person_Name = ObjectPersonModelData.ObjectPersonData.Person_Name;
                            _ObjectPerson.PersonType = ObjectPersonModelData.ObjectPersonData.PersonType;
                            _ObjectPerson.Description = ObjectPersonModelData.ObjectPersonData.Description;
                            _ObjectPerson.Gender = ObjectPersonModelData.ObjectPersonData.Gender;
                            _ObjectPerson.Title = ObjectPersonModelData.ObjectPersonData.Title;
                            _ObjectPerson.Telephone = ObjectPersonModelData.ObjectPersonData.Telephone;


                            if (ObjectPersonModelData.ObjectPersonData.Birthday != null)
                            {

                                _ObjectPerson.Birthday = ObjectPersonModelData.ObjectPersonData.Birthday;
                            }

                            else
                            {
                                _ObjectPerson.Birthday = DateTime.Today;
                            }

                            _ObjectPerson.Position = ObjectPersonModelData.ObjectPersonData.Position;
                            _ObjectPerson.Email = ObjectPersonModelData.ObjectPersonData.Email;
                            _ObjectPerson.AddID_FK = ObjectPersonModelData.ObjectPersonData.AddID_FK;
                            _ObjectPerson.C2DCodeType = ObjectPersonModelData.ObjectPersonData.C2DCodeType;

                            _ObjectPerson.PersonDetailsUrl = "";

                            ctx.ObjectPerson.Add(_ObjectPerson);

                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {


                            var abc = ex.Message;

                        }


                        //_ObjectTbl.ContentsURL = _ObjectTbl.ContentsURL + "&PersonID=" + _ObjectPerson.PersonID_PK.ToString();
                        _ObjectPerson.PersonDetailsUrl = BaseUrlStr + PersonDetailsUrlstr + _ObjectPerson.ObjectID_FK.ToString();
                        ObjectPersonModelData.Object_id = _ObjectTbl.Object_id;
                        ObjectPersonModelData.ObjectPersonData.PersonID_PK = _ObjectPerson.PersonID_PK;
                        ctx.SaveChanges();
                    }
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

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, ObjectPersonModelData);

            return response;

        }
    }


}

