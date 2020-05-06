using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Canada2DCode.DataBaseEngine;

namespace Canada2DCode.Models
{
    public class CustomerOrders
    {
        public Customers Customer { get; set; }
        public List<Orders> Orders { get; set; }
    }
}