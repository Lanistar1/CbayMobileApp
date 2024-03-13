using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Shopping.Shipping;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Shopping
{
    public class AddressViewModel : BaseViewModel
    {
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string zipCode;
        public string ZipCode
        {
            get => zipCode;
            set
            {
                zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public AddressViewModel(INavigation navigation)
        {

            Navigation = navigation;

            AddShippingAddressCommand = new Command(async () => await AddShippingAddressCommandExecute());

        }

        public Command AddShippingAddressCommand { get; }
        public Command UpdateShippingAddressCommand { get; }

        public async Task AddShippingAddressCommandExecute()
        {
            try
            {
                await LoadingPopup.Instance.Show("Saving address...");

                HttpClient client = new HttpClient();

                AddShippingAddressRequestModel requestPayload = new AddShippingAddressRequestModel()
                { Address = Address, City = City, FirstName = FirstName, LastName = LastName, Nigeria = Country, Phone = Phone, ZipCode = ZipCode };

                string payloadJson = JsonConvert.SerializeObject(requestPayload);

                Console.WriteLine(payloadJson);

                string url = Global.AddShippingAddressUrl;
                Console.WriteLine(url);
                StringContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response;
                response = await client.PostAsync(url, content);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await Application.Current.MainPage.DisplayAlert("Address added Successfully", "", "OK");
                    await Navigation.PopAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await MessagePopup.Instance.Show("Session expire");

                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Something went wrong", "Please try again later", "OK");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }

    }

}
