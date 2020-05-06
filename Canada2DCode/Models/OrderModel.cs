using System;
using System.Collections.Generic;
using System.Linq;

namespace Canada2DCode.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public CustomerModel Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderDetailModel> OrderDetails { get; set; }
    }
}
