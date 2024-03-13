using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Shopping.Shipping;
using CBayMobileApp.ViewModels.Shopping;
using CBayMobileApp.Views.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static CBayMobileApp.Helpers.Global;

namespace CBayMobileApp.Views.Cart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmCartOrder : ContentPage
    {
        public ConfirmCartOrder(ObservableCollection<CartItem> selectedItems)
        {
            InitializeComponent();
            BindingContext = new CartOrderConfirmationViewModel(Navigation, selectedItems);

        }

        private void To_NewAddress(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewAddress());

            //Application.Current.MainPage = new NavigationPage(new NewAddress());

        }


        //private void AddressPicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Picker picker = sender as Picker;
        //    if (picker.SelectedItem != null)
        //    {
        //        selectedAddress.Text = (picker.SelectedItem as AddressData).address;
        //        Global.Address = selectedAddress.Text;

        //        selectedName.Text = (picker.SelectedItem as AddressData).fullName;
        //        Global.FullName = selectedName.Text;

        //        selectedCity.Text = (picker.SelectedItem as AddressData).city;
        //        Global.City = selectedCity.Text;

        //        selectedCountry.Text = (picker.SelectedItem as AddressData).country;
        //        Global.Country = selectedCountry.Text;

        //        selectedPhone.Text = (picker.SelectedItem as AddressData).phone;
        //        Global.Phone = selectedPhone.Text;


        //        //Global.SelectedCategory = selectedAddress;
        //    }
        //}
    }
}