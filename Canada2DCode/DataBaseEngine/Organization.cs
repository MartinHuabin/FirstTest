//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Canada2DCode.DataBaseEngine
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organization
    {
        public int OrganizationID_PK { get; set; }
        public string OrganizationName { get; set; }
        public Nullable<int> OrganizationAddCode { get; set; }
        public Nullable<int> UserOrganizationID { get; set; }
        public string Note { get; set; }
        public Nullable<int> AdminCode { get; set; }
        public Nullable<System.DateTime> CreateDate_DT { get; set; }
        public string CreatedBy_NV { get; set; }
        public Nullable<System.DateTime> DeleteDate_DT { get; set; }
        public string DeletedBy_NV { get; set; }
    }
}
