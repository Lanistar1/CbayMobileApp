using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Wallet
{

    public class GetWalletTransactionResponseModel
    {
        public string transactionID { get; set; }
        public string transactionNo { get; set; }
        public string narration { get; set; }
        public string amount { get; set; }
        public string refWalletNo { get; set; }
        public string transxnType { get; set; }
        public string transxnDate { get; set; }
        public string toWalletID { get; set; }
        public string fromWalletID { get; set; }
    }


}
