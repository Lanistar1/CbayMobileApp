using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using CBayMobileApp.Views.Cart;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static CBayMobileApp.Helpers.Global;

namespace CBayMobileApp.ViewModels.Shopping
{
    public class CartViewModel : BaseViewModel
    {
        private ObservableCollection<CartItem> cartItem;
        public ObservableCollection<CartItem> CartItem
        {
            get => cartItem;
            set
            {
                cartItem = value;
                OnPropertyChanged(nameof(CartItem));
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private string vendorName;
        public string VendorName
        {
            get => vendorName;
            set
            {
                vendorName = value;
                OnPropertyChanged(nameof(VendorName));
            }
        }

        private string productQuantity;
        public string ProductQuantity
        {
            get => productQuantity;
            set
            {
                productQuantity = value;
                Global.ProductQuantity = ProductQuantity;
                OnPropertyChanged(nameof(ProductQuantity));
            }
        }

        private int productPrice;
        public int ProductPrice
        {
            get => productPrice;
            set
            {
                productPrice = value;
                OnPropertyChanged(nameof(ProductPrice));
            }
        }

        public CartViewModel(INavigation navigation)
        {

            Navigation = navigation;

            CartItem = Global.myCarts;

            RefreshCommand = new Command(async () => await RefreshCommandExecute());
            NextCommand = new Command(async () => await NextCommandExecute());
            //NextCommand = new Command<CartItem>(async (model) => await NextCommandExecute(model));

            DeleteCommand = new Command<CartItem>((model) => OnTapped(model));

        }

        public Command RefreshCommand { get; }
        public Command NextCommand { get; }
        public Command DeleteCommand { get; set; }




        private Task OnTapped(CartItem model)
        {
            // Find the item in the cart with the specified productID
            CartItem itemToRemove = Global.myCarts.FirstOrDefault(item => item.productID == model.productID);

            if (itemToRemove != null)
            {
                // Remove the item from the cart
                Global.myCarts.Remove(itemToRemove);

                OnPropertyChanged(nameof(CartItem));

            }

            return Task.CompletedTask;
        }

        public async Task RefreshCommandExecute()
        {
            CartItem = Global.myCarts;
            ProductPrice = CartItem.FirstOrDefault().productPrice;

            IsRefreshing = false;
        }

        private async Task NextCommandExecute()
        {
            try
            {

                await LoadingPopup.Instance.Show("Loading...");

                CartItem = Global.myCarts;


                //await Navigation.PushModalAsync(new ConfirmCartOrder(CartItem), true);

                App.Current.MainPage = new NavigationPage(new ConfirmCartOrder(CartItem));
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
