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

namespace CBayMobileApp.Views.Cart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        WalletViewModel pageViewModel = null;

        public Cart()
        {
            pageViewModel = new WalletViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_DeliveryAddress(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeliveryAddress());
        }
    }
}