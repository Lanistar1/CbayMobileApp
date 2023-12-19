using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Common;
using CBayMobileApp.Models.Shopping;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views.Products;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Shopping
{

    public class GetAllProductViewModel : BaseViewModel
    {
        public GetAllProductViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Task _tsk = UpdatePageBindings();

            TappedCommand = new Command<GetAllProductData>(async (model) => await GetTappedExecute(model));

        }


        #region Bindings

        private List<GetAllProductData> SelectedItems = new List<GetAllProductData>();


        private List<OrderDetailData> userOrder;
        public List<OrderDetailData> UserOrder
        {
            get => userOrder;
            set
            {
                userOrder = value;
                OnPropertyChanged(nameof(UserOrder));
            }
        }

        private List<GetAllProductData> productData;
        public List<GetAllProductData> ProductData
        {
            get => productData;
            set
            {
                productData = value;
                OnPropertyChanged(nameof(ProductData));
            }
        }

        private List<SearchProductData> searchProduct;
        public List<SearchProductData> SearchProduct
        {
            get => searchProduct;
            set
            {
                searchProduct = value;
                OnPropertyChanged(nameof(SearchProduct));
            }
        }

        private string productName;
        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string productID;
        public string ProductID
        {
            get => productID;
            set
            {
                productID = value;
                OnPropertyChanged(nameof(ProductID));
            }
        }


        private int _limit = 3;

        private string searchEntry = string.Empty;
        private CancellationTokenSource searchDelayTokenSource;
        public string SearchEntry
        {
            get => searchEntry;
            set
            {
                searchEntry = value;

                // Cancel the previous search delay task if it exists
                searchDelayTokenSource?.Cancel();

                // Create a new cancellation token source
                searchDelayTokenSource = new CancellationTokenSource();

                // Delay the execution of the command to allow the value to update
                Task.Delay(TimeSpan.FromMilliseconds(200), searchDelayTokenSource.Token)
                    .ContinueWith(_ =>
                    {
                        if (!searchDelayTokenSource.Token.IsCancellationRequested)
                        {
                            SearchEntryTextChangedCommand.Execute(searchEntry);
                        }
                    });

                OnPropertyChanged(nameof(SearchEntry));
            }
        }
        #endregion

        #region Commands
        public Command PlaceOrderCommand { get; }
        public Command TappedCommand { get; }
        public Command SearchEntryTextChangedCommand => new Command<string>((searchEntry) => SearchBar_TextChanged(searchEntry));

        #endregion


        #region functions, methods, events and Navigations
        // search product imolementation

        private async Task SearchBar_TextChanged(string _searchEntry)
        {
            if (_searchEntry.Length == _limit)
            {
                await FilterProductList(_searchEntry);

            }
            else
            {
                return;
            }
        }

        private async Task FilterProductList(string _searchEntry)
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading...");

                HttpClient client = new HttpClient();

                string url = $"{Global.SearchUrl}/{_searchEntry}";

                Console.WriteLine(url);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response = await client.GetAsync(url);

                Console.WriteLine(response);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SearchProductResponseModel data = JsonConvert.DeserializeObject<SearchProductResponseModel>(result);
                    Console.WriteLine(data);

                    SearchProduct = data.data;

                    List<SelectItemModel> availableProduct = new List<SelectItemModel>();

                    foreach (SearchProductData productDetail in SearchProduct)
                    {
                        availableProduct.Add(new SelectItemModel(productDetail.searchValueID, productDetail.searchValue));

                    }


                    var popup = new SelectItemPickerPopup(availableProduct);

                    await PopupNavigation.Instance.PushAsync(popup);

                    var searchResult = await popup.PopupClosedTask;
                    ProductName = searchResult.Item1;
                    ProductID = searchResult.Item2;

                    //App.Current.MainPage = new NavigationPage(new BuyerTabbed());

                    await Device.InvokeOnMainThreadAsync(() => App.Current.MainPage.Navigation.PushAsync(new ProductDetailByID(ProductID), true));
                    //await PopupNavigation.Instance.PopAsync();

                    await Navigation.PopAllPopupAsync();
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




        private async Task UpdatePageBindings()
        {
            await FetchAllProductsAsync();

        }

        private async Task GetTappedExecute(GetAllProductData model)
        {
            try
            {
                var mod = model;

                model.isSelected = model.isSelected ? false : true;
                if (SelectedItems.Count > 0)
                {
                    SelectedItems.Clear();
                }
                SelectedItems.Add(model);

                await Navigation.PushAsync(new ProductDetail(SelectedItems), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task FetchAllProductsAsync()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Products...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetAllProductAsync();
                if (ResponseData != null)
                {
                    if (ResponseData != null)
                    {
                        ProductData = ResponseData.data;
                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching products detail. Do you want to RETRY?";
                    //await MessagePopup.Instance.Show(
                    //    message: message);

                }
                else
                {
                    //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                string message = "Error fetching products detail. Do you want to RETRY?";
                await MessagePopup.Instance.Show(
                    message: message);
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }

        #endregion


    }

}
