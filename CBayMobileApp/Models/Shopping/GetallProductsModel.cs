using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class GetallProductsModel
    {
        public string productID { get; set; }
        public string description { get; set; }
        public string vendorID { get; set; }
        public double weightKG { get; set; }
        public string sizeLBHcm { get; set; }
        public int price { get; set; }
        public string defaultPictureLocation { get; set; }
        public string otherPictureLocation { get; set; }
        public int quantityAvailable { get; set; }
    }


}
