using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class GetAllProductData
    {
        public string productID { get; set; }
        public string name { get; set; }
        public int productPrice { get; set; }
        public int listingPrice { get; set; }
        public object description { get; set; }
        public bool isVerified { get; set; }
        public string defaultPictureLocation { get; set; }
        public object otherPictureLocationCSV { get; set; }
        public string restuarantID { get; set; }
        public object currency { get; set; }
        public string categoryID { get; set; }
        public int weightKG { get; set; }
        public int discountPer { get; set; }
        public int timeToPrepMins { get; set; }
        public bool isSelected { get; set; }
    }


    public class GetallProductsModel
    {
        public List<GetAllProductData> data { get; set; }
    }


}
