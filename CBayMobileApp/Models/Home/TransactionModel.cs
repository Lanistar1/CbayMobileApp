using CBayMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Home
{
    public class TransactionModel : BaseViewModel
    {
        public string description { get; set; }
        public string amount { get; set; }
        public string time { get; set; }

        private string plusImage;
        public string PlusImage
        {
            get { return plusImage; }
            set
            {
                plusImage = value;
                OnPropertyChanged(nameof(PlusImage));
            }
        }
    }

}
