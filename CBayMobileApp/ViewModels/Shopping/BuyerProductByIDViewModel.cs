using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using static CBayMobileApp.Helpers.Global;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using CBayMobileApp.Models.Shopping;

namespace CBayMobileApp.ViewModels.Shopping
{
    public class BuyerProductByIDViewModel : BaseViewModel
    {
        public BuyerProductByIDViewModel(INavigation navigation, string ProductID)
        {
            Navigation = navigation;

            Task _task = GetProductByID(ProductID);

            NextCommand = new Command<productByIdData>(async (model) => await NextCommandExecute(model));

            AddCartCommand = new Command<productByIdData>(async (model) => await AddCartCommandExecute(model));
            DeleteCommand = new Command<CartItem>((model) => OnTapped(model));

        }

        private productByIdData productByID;
        public productByIdData ProductByID
        {
            get => productByID;
            set
            {
                productByID = value;
                OnPropertyChanged(nameof(ProductByID));
            }
        }

        private List<CartItem> myCart;
        public List<CartItem> MyCart
        {
            get => myCart;
            set
            {
                myCart = value;
                OnPropertyChanged(nameof(MyCart));
            }
        }

        public List<productByIdData> selectedItems;
        public productByIdData model;

        public List<productByIdData> SelectedItems
        {
            get => selectedItems;
            set
            {
                selectedItems = value;
            }
        }

        public Command NextCommand { get; }
        public Command AddCartCommand { get; }
        public Command DeleteCommand { get; set; }

        private Task OnTapped(CartItem model)
        {
            var g = from y in Global.myCarts where y.productID == model.productID select y.productID;
            Console.WriteLine("Not aapplicable");
            Global.myCarts.Remove((CartItem)g);
            return Task.CompletedTask;
        }

        private async Task NextCommandExecute(productByIdData model)
        {

            try
            {
                await LoadingPopup.Instance.Show("Loading...");

                var mod = model;

                model.isSelected = model.isSelected ? false : true;
                if (SelectedItems.Count > 0)
                {
                    SelectedItems.Clear();
                }
                SelectedItems.Add(model);


                //await Navigation.PushAsync(new OrderConfirmationByID(SelectedItems), true);
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

        private async Task AddCartCommandExecute(productByIdData model)
        {
            try
            {

                var mod = model;
                if (Helpers.Global.myCarts == null) { Helpers.Global.myCarts = new ObservableCollection<CartItem>(); }
                Helpers.Global.myCarts.Add(new CartItem
                {
                    productID = model.productID,
                    quantity = 1,
                    productName = model.name,
                    defaultPictureLocation = model.defaultPictureLocation,
                    productPrice = model.price
                });
                Console.WriteLine("kcsgcahjgha");


                //await Navigation.PushPopupAsync(new AddToCartPopup());

                await PopupNavigation.Instance.PushAsync(new AddToCartPopup());



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                //await LoadingPopup.Instance.Hide();
            }
        }

        private async Task GetProductByID(string ProductID)
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading...");

                HttpClient client = new HttpClient();

                string url = $"{Global.GetProductByIDUrl}/{ProductID}";

                Console.WriteLine(url);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response = await client.GetAsync(url);

                Console.WriteLine(response);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    BuyerProductByIDModel data = JsonConvert.DeserializeObject<BuyerProductByIDModel>(result);
                    //Console.WriteLine(data);

                    ProductByID = data.data;
                }
                else
                {
                    Console.WriteLine("Someting went wrong");
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
