using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Wallet
{
    public class ValidateData
    {
        public string account_number { get; set; }
        public string account_name { get; set; }
    }

    public class ValidateAccountResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public ValidateData data { get; set; }
    }
}
