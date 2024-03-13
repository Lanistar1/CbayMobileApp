using CBayMobileApp.ViewModels.AuthFlow;
using CBayMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CBayMobileApp.ViewModels.Wallets;
using CBayMobileApp.ViewModels.Shopping;

namespace CBayMobileApp.Views.Cart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        CartViewModel pageViewModel = null;

        public Cart()
        {
            pageViewModel = new CartViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_DeliveryAddress(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeliveryAddress());
        }
    }
}