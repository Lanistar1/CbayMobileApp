using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class GetAllProductData
    {
        public string productID { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int listingPrice { get; set; }
        public string descriptionHTML { get; set; }
        public string size { get; set; }
        public bool isVerified { get; set; }
        public string defaultPictureLocation { get; set; }
        public object otherPictureLocationCSV { get; set; }
        public object currency { get; set; }
        public string categoryID { get; set; }
        public double weightKG { get; set; }
        public bool isSelected { get; set; }
    }


    public class GetallProductsModel
    {
        public List<GetAllProductData> data { get; set; }
    }


}
