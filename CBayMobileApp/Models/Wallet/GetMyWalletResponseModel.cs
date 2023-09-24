using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Wallet
{
    public class WalletData
    {
        public int balance { get; set; }
        public string walletID { get; set; }
        public string walletNo { get; set; }
        public string currency { get; set; }
    }

    public class GetMyWalletResponseModel
    {
        public WalletData data { get; set; }
    }


}
