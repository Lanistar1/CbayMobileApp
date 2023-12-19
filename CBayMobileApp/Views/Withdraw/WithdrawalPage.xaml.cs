using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Wallet;
using CBayMobileApp.ViewModels.Wallets;
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
    public partial class WithdrawalPage : ContentPage
    {
        private string selectedBank;
        private string BankName;
        private string selectedWallet;

        public WithdrawalPage()
        {
            InitializeComponent();
            BindingContext = new withdrawalViewModel(Navigation);

        }

        private void BankListPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            if (picker.SelectedItem != null)
            {
                selectedBank = (picker.SelectedItem as BankData).code;

                BankName = (picker.SelectedItem as BankData).name;
                Global.bank = BankName;
                Global.bankName = selectedBank;
            }
        }
    }
}