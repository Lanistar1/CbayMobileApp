using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Wallet
{

    public class GetMyWalletResponseModel
    {
        public string walletID { get; set; }
        public string walletNo { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
    }


}
