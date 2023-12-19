using CBayMobileApp.Helpers;
using CBayMobileApp.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Withdraw
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FundWallet : ContentPage
    {
        public string firstName;
        public string email;
        public string phoneNumber;
        public string newAmount;

        public FundWallet()
        {
            InitializeComponent();
        }

        private async void To_flutterwave(object sender, EventArgs e)
        {
            var userData = Global.UserProfileData;
            firstName = userData.firstName;
            email = userData.emailAddress;
            newAmount = fundAmount.Text;
            phoneNumber = userData.phoneNo;

            string url = $"https://flutterwave.cbays.ng/?amount={newAmount}&name={firstName}&email={email}&phone={phoneNumber}"; // Replace with your desired URL

            await Navigation.PushAsync(new WebviewPage(url));

            //await Launcher.OpenAsync(url);
        }
    }
}