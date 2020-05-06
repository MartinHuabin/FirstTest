using System;
using System.Collections.Generic;
using System.Linq;

namespace Canada2DCode.Models
{
    public static class DataFactory
    {
        public static IEnumerable<CustomerModel> GetCustomers()
        {
            return new List<CustomerModel>()
            {
                new CustomerModel() { CustomerId = 1, FirstName = "Miguel", LastName = "Castro", Email = "miguelcastro67@gmail.com" },
                new CustomerModel() { CustomerId = 2, FirstName = "John", LastName = "Petersen", Email = "johnvpetersen@gmail.com" },
                new CustomerModel() { CustomerId = 3, FirstName = "Brian", LastName = "Noyes", Email = "briannoyes@gmail.com" },
                new CustomerModel() { CustomerId = 4, FirstName = "Andrew", LastName = "Brust", Email = "andrewbrust@gmail.com" },
                new CustomerModel() { CustomerId = 5, FirstName = "Rocky", LastName = "Lhotka", Email = "rockylhotka@gmail.com" }
            };
        }

        public static IEnumerable<ProductModel> GetProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel() { ProductId = 1, Description = "Asus Motherboard", UnitPrice = 499.00M },
                new ProductModel() { ProductId = 2, Description = "NVidia 780ti video card", UnitPrice = 699.00M },
                new ProductModel() { ProductId = 3, Description = "Samsung 1TB SSD", UnitPrice = 649.00M },
                new ProductModel() { ProductId = 4, Description = "Cooler Master Cosmos II case", UnitPrice = 189.00M }
            };
        }

        public static IEnumerable<OrderModel> GetOrders()
        {            
            return new List<OrderModel>()
            {
                new OrderModel() { OrderId = 1, OrderNumber = "1002983", OrderDate = new DateTime(2014, 10, 4),
                    Customer = DataFactory.GetCustomers().FirstOrDefault(item => item.CustomerId == 1), OrderDetails = new List<OrderDetailModel>()
                {
                    new OrderDetailModel() { OrderDetailId = 1, Product = DataFactory.GetProducts().FirstOrDefault(item => item.ProductId == 1), Quantity = 1, UnitPrice = 499.00M },
                    new OrderDetailModel() { OrderDetailId = 2, Product = DataFactory.GetProducts().FirstOrDefault(item => item.ProductId == 4), Quantity = 1, UnitPrice = 189.00M }
                }},
                new OrderModel() { OrderId = 2, OrderNumber = "1003892", OrderDate = new DateTime(2014, 10, 12),
                    Customer = DataFactory.GetCustomers().FirstOrDefault(item => item.CustomerId == 3), OrderDetails = new List<OrderDetailModel>()
                {
                    new OrderDetailModel() { OrderDetailId = 3, Product = DataFactory.GetProducts().FirstOrDefault(item => item.ProductId == 1), Quantity = 1, UnitPrice = 499.00M },
                    new OrderDetailModel() { OrderDetailId = 4, Product = DataFactory.GetProducts().FirstOrDefault(item => item.ProductId == 2), Quantity = 2, UnitPrice = 699.00M },
                    new OrderDetailModel() { OrderDetailId = 5, Product = DataFactory.GetProducts().FirstOrDefault(item => item.ProductId == 3), Quantity = 2, UnitPrice = 649.00M },
                    new OrderDetailModel() { OrderDetailId = 6, Product = DataFactory.GetProducts().FirstOrDefault(item => item.ProductId == 4), Quantity = 1, UnitPrice = 189.00M }
                }}
            };
        }
    }
}
