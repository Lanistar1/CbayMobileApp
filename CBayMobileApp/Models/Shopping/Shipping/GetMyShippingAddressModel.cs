using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping.Shipping
{
    public class AddressData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string maplocation { get; set; }

        public string fullName
        {
            get
            {
                return this.firstName + " " + this.lastName;
            }
        }
    }

    public class GetMyShippingAddressModel
{
        public List<AddressData> data { get; set; }
    }
}
