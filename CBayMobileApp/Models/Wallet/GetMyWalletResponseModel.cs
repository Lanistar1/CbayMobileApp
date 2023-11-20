using CBayMobileApp.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using static Xamarin.Forms.Internals.Profile;

namespace CBayMobileApp.Models.Wallet
{
    public class WalletData
    {
        public int balance { get; set; }
        public string walletID { get; set; }
        public string walletNo { get; set; }
        public string currency { get; set; }
        public bool isCompensation { get; set; }

        public string DisplayAmount
        {
            get
            {
                return StringHelper.FormatAmountWithNaira(balance);
            }
            set => DisplayAmount = value;
        }
    }

    public class GetMyWalletResponseModel
    {
        public List<WalletData> data { get; set; }
    }


}
