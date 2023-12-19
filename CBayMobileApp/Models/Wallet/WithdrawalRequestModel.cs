using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Wallet
{
    public class WithdrawalRequestModel
    {
        public string FromWalletID { get; set; }
        public string AccountNo { get; set; }
        public double Amount { get; set; }
        public string BankCode { get; set; }
    }
}
