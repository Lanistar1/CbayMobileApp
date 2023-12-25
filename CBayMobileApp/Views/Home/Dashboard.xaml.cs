using CBayMobileApp.Utils;
using CBayMobileApp.ViewModels.Home;
using CBayMobileApp.ViewModels.Wallets;
using CBayMobileApp.Views.Withdraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new WalletViewModel(Navigation);
        }

        private void To_recentEarning(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecentEarning());
        }

        private async void To_addMoney(object sender, EventArgs e)
        {

            //await MessagePopup.Instance.Show("Waiting for payment gateway.");
            await Navigation.PushAsync(new FundWallet());

        }

        private void To_WithdrawPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WithdrawalPage());

        }
    }
}