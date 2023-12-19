using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class productByIdData
    {
        public string productID { get; set; }
        public string name { get; set; }
        public int productPrice { get; set; }
        public int listingPrice { get; set; }
        public string description { get; set; }
        public bool isVerified { get; set; }
        public string defaultPictureLocation { get; set; }
        public string otherPictureLocationCSV { get; set; }
        public string vendorID { get; set; }
        public string vendorName { get; set; }
        public string currency { get; set; }
        public string currencySymbol { get; set; }
        public int formerPrice { get; set; }
        public int quantityAvailable { get; set; }
        public bool inStock { get; set; }
        public string categoryID { get; set; }
        public bool isSelected { get; set; }
    }

    public class BuyerProductByIDModel
    {
        public productByIdData data { get; set; }
    }

}
