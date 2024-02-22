using CBayMobileApp.Models.Shopping;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CBayMobileApp.Models.Wallet
{

    public class GetWalletTransactionData
    {
        public string transactionID { get; set; }
        public string transactionNo { get; set; }
        public string narration { get; set; }
        public double amount { get; set; }
        public string externalRefNo { get; set; }
        public string refWalletNO { get; set; }
        public DateTime transxnDate { get; set; }

        public string FormattedAmount { get {
                return this.amount.ToString("C", new System.Globalization.CultureInfo("en-US")).Replace("$", "");
            } }
        public string transxType { get; set; }
        public string toWalletID { get; set; }
        public double previousToWalletBalance { get; set; }
        public double presentToWalletBalance { get; set; }
        public string fromWalletID { get; set; }

        public string newDate
        {
            get
            {
                return transxnDate.Date.ToShortDateString();
                //Console.WriteLine(createdAt);
                //return newDate;
            }
            set
            {
                newDate = value;
            }
        }

        public TimeSpan newTime
        {
            get
            {
                return transxnDate.TimeOfDay;
                //Console.WriteLine(createdAt);
                //return newDate;
            }
            set
            {
                newTime = value;
            }
        }

        public double Amount
        {
            get
            {
                if (transxType == "DEBIT")
                {
                    SubColor = Color.FromHex("BE023C");
                    return amount;
                }
                else
                {
                    SubColor = Color.FromHex("0C9A00");
                    return amount;
                }
            }
            set
            {
                Amount = value;
            }
        }


        public ImageSource TransImage
        {
            get
            {
                if (transxType.Contains("CREDIT") || transxType == "CREDIT")
                {
                    ImageSource source1 = ImageSource.FromFile("credit.png");
                    return source1;
                }
                else
                {
                    ImageSource source = ImageSource.FromFile("debit.png");
                    return source;
                }

            }
            set { TransImage = value; }
        }


        public Color SubColor { get; set; }
    }

    public class GetWalletTransactionResponseModel
    {
        public List<GetWalletTransactionData> data { get; set; }
    }
}
