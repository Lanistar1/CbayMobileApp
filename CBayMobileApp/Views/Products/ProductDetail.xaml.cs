﻿using CBayMobileApp.Models.Shopping;
using CBayMobileApp.ViewModels.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        public ProductDetail(List<GetAllProductData> selectedItems)
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(Navigation, selectedItems);

        }
    }
}