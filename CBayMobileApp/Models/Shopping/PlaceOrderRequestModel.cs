using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class Orderdetail
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }

    public class PlaceOrderRequestModel
    {
        public List<Orderdetail> orderdetails { get; set; }
        public OrderShippingAddress ShippingAddress { get; set; }
        public string DeliveryInstruction { get; set; }
        public string WalletID { get; set; }

    }

    public class OrderShippingAddress
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

}
