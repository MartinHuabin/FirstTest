using System;
using Canada2DCode.DataBaseEngine;

namespace Canada2DCode.Models
{
    public class ObjectPersonModel
    {

        public int Object_id { get; set; }
        public string Object_name { get; set; }
      
        public int Category_id { get; set; }
        public short Model_year { get; set; }
     
        public Nullable<int> OrganizationID_FK { get; set; }
        public string C2DMACode { get; set; }
        public byte[] QRCode { get; set; }
        public string ContentsURL { get; set; }
        public byte[] PrivateKey { get; set; }
        public Nullable<int> OpenToRole { get; set; }
        public Nullable<System.Guid> BlockID { get; set; }
        public Nullable<System.Guid> NextChainID { get; set; }
        public Nullable<System.DateTime> CreatedDate_DT { get; set; }
        public string CreatedBy_NV { get; set; }
        public Nullable<System.DateTime> DeleteDate_DT { get; set; }
        public string DeleteByName_NV { get; set; }


        public ObjectPerson ObjectPersonData { get; set; }


    }
}