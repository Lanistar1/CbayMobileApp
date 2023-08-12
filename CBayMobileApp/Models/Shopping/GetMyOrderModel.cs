using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{

    public class OrderDetailData
    {
        public string productID { get; set; }
        public int quantity { get; set; }
        public string productName { get; set; }
    }

    public class GetMyOrderModel
    {
        public string orderID { get; set; }
        public List<OrderDetailData> orderDetails { get; set; }
        public ShippingAddressData shippingAddress { get; set; }
        public string deliveryInstruction { get; set; }
        public string orderStatus { get; set; }
    }

    public class ShippingAddressData
    {
        public string address { get; set; }
        public string name { get; set; }
        public string phoneNo { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }


}
