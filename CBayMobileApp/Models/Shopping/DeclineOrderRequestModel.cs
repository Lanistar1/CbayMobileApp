using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Shopping
{
    public class DeclineOrderRequestModel
    {
        public string OrderID { get; set; }
        public string Reason { get; set; }
    }


}
