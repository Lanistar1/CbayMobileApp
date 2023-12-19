using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Wallet
{
    public class BankData
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class GetAllBanksModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<BankData> data { get; set; }
    }
}
