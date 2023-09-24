using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Shopping;
using CBayMobileApp.Models.Shopping.Shipping;
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
    public partial class OrderConfirmation : ContentPage
    {
        public OrderConfirmation(List<GetAllProductData> selectedItems)
        {
            InitializeComponent();
            BindingContext = new OrderConfirmationViewModel(Navigation, selectedItems);

        }

        private void To_NewAddress(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewAddress());
        }


        private void AddressPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            if (picker.SelectedItem != null)
            {
                selectedAddress.Text = (picker.SelectedItem as AddressData).address;
                Global.Address = selectedAddress.Text;

                selectedName.Text = (picker.SelectedItem as AddressData).fullName;
                Global.FullName = selectedName.Text;

                selectedCity.Text = (picker.SelectedItem as AddressData).city;
                Global.City = selectedCity.Text;

                selectedCountry.Text = (picker.SelectedItem as AddressData).country;
                Global.Country = selectedCountry.Text;

                selectedPhone.Text = (picker.SelectedItem as AddressData).phone;
                Global.Phone = selectedPhone.Text;


                //Global.SelectedCategory = selectedAddress;
            }
        }
    }
}