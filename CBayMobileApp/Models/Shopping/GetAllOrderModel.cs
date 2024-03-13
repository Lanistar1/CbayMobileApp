using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{

    public class OrderDetail
    {
        public string productID { get; set; }
        public int quantity { get; set; }
        public int unitPrice { get; set; }
        public string productName { get; set; }
    }

    public class GetAllOrderModel
    {
        public string orderID { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public string deliveryInstruction { get; set; }
        public string orderStatus { get; set; }
    }

    public class ShippingAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }


}
