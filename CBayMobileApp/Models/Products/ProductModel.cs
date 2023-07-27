using CBayMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Products
{
    public class ProductModel : BaseViewModel
    {
        public string title { get; set; }
        public string amount { get; set; }

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
