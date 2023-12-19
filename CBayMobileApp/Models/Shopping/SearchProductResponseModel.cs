using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class SearchProductData
    {
        public string searchTerm { get; set; }
        public string searchValue { get; set; }
        public string searchValueID { get; set; }
        public bool isProduct { get; set; }
        public bool isCategory { get; set; }
    }

    public class SearchProductResponseModel
    {
        public List<SearchProductData> data { get; set; }
    }
}
