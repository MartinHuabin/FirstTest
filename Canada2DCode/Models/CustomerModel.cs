using System;
using System.Collections.Generic;
using System.Linq;
using Canada2DCode.DataBaseEngine;

namespace Canada2DCode.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Conditions_BT { get; set; }

        public int? AddID_FK { get; set; }
        public string Telephone { get; set; }

        public List<Addresses> AddressesList { get; set; }


    }
}
