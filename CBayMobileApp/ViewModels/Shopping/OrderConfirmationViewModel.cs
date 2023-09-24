using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Shopping.Shipping;
using CBayMobileApp.Models.Shopping;
using CBayMobileApp.Models.Wallet;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CBayMobileApp.Views.Identity;

namespace CBayMobileApp.ViewModels.Shopping
{
    public class OrderConfirmationViewModel : BaseViewModel
    {
        public List<AddressData> shippingAddress;
        public List<AddressData> ShippingAddress
        {
            get => shippingAddress;
            set
            {
                shippingAddress = value;
                OnPropertyChanged(nameof(ShippingAddress));

            }
        }


        //public List<WalletData> buyerWalletData;
        //public List<WalletData> BuyerWalletData
        //{
        //    get => buyerWalletData;
        //    set
        //    {
        //        buyerWalletData = value;
        //        OnPropertyChanged(nameof(BuyerWalletData));

        //    }
        //}


        public List<GetAllProductData> selectedItems;
        public List<GetAllProductData> SelectedItems
        {
            get => selectedItems;
            set
            {
                selectedItems = value;
            }
        }

        private List<GetAllProductData> productDetail;
        public List<GetAllProductData> ProductDetail
        {
            get => productDetail;
            set
            {
                productDetail = value;
                OnPropertyChanged(nameof(ProductDetail));
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


        private string approval;
        public string Approval
        {
            get => approval;
            set
            {
                approval = value;
                OnPropertyChanged(nameof(Approval));
            }
        }

        private string deliveryInstruction;
        public string DeliveryInstruction
        {
            get => deliveryInstruction;
            set
            {
                deliveryInstruction = value;
                OnPropertyChanged(nameof(DeliveryInstruction));
            }
        }

        private string walletNo;
        public string WalletNo
        {
            get => walletNo;
            set
            {
                walletNo = value;
                OnPropertyChanged(nameof(WalletNo));
            }
        }

        private string currency;
        public string Currency
        {
            get => currency;
            set
            {
                currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }


        private string quantity;
        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }


        private int availableBalance;
        public int AvailableBalance
        {
            get => availableBalance;
            set
            {
                availableBalance = value;
                OnPropertyChanged(nameof(AvailableBalance));
            }
        }

        public OrderConfirmationViewModel(INavigation navigation, List<GetAllProductData> selectedItems)
        {

            Navigation = navigation;

            Task _tasks = GetMyWalletExecute();

            PlaceOrderCommand = new Command(async () => await PlaceOrderCommandExecute());

            SelectedItems = selectedItems;

            ProductDetail = new List<GetAllProductData>(SelectedItems);

            ProductPrice = ProductDetail.FirstOrDefault().productPrice;
            //Global.PriceTag = ProductPrice;

        }

        public Command PlaceOrderCommand { get; }
        public Command FilterCommand { get; }

        public Command AddShippingAddressCommand { get; }
        public Command UpdateShippingAddressCommand { get; }

        public async Task GetMyShippingAddressExecute()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading...");

                HttpClient client = new HttpClient();

                string url = Global.GetMyShippingAddressUrl;

                Console.WriteLine(url);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response = await client.GetAsync(url);

                response = await client.GetAsync(url);

                Console.WriteLine(response);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetMyShippingAddressModel data = JsonConvert.DeserializeObject<GetMyShippingAddressModel>(result);
                    // Console.WriteLine("passedjiojiojiojio");
                    Console.WriteLine(data);

                    ShippingAddress = data.data;

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


        public async Task GetMyWalletExecute()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading details...");

                HttpClient client = new HttpClient();

                //string url = $"{Global.MyWalletUrl}?currency={Approval}";

                string url = Global.WalletUrl;

                Console.WriteLine(url);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response = await client.GetAsync(url);
                response = await client.GetAsync(url);
                Console.WriteLine(response);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetMyWalletResponseModel data = JsonConvert.DeserializeObject<GetMyWalletResponseModel>(result);
                    // Console.WriteLine("passedjiojiojiojio");
                    Console.WriteLine(data);

                    

                    var test = data.data.walletID;
                    Global.WalletId = test;
                    WalletNo = data.data.walletNo;

                    AvailableBalance = data.data.balance;
                    Global.Balance = AvailableBalance;

                    //await GetMyWalletBalanceExecute(Approval);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
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


        public async Task PlaceOrderCommandExecute()
        {
            try
            {
                await LoadingPopup.Instance.Show("Placing order...");

                HttpClient client = new HttpClient();

                var newBalance = Global.Balance;
                //var newPrice = Global.PriceTag;

                if (newBalance < ProductPrice)
                {
                    await MessagePopup.Instance.Show("Insufficient fund.");
                    return;
                }

                var details = ProductDetail;

                if (Quantity == null)
                {
                    await MessagePopup.Instance.Show("Please enter quantity.");
                    return;
                }

                int newQuantity = int.Parse(Quantity);


                List<string> detailLot = new List<string>();
                var newOrder = new List<Orderdetail>();

                foreach (var item in details)
                {
                    var productID = item.productID;
                    detailLot.Add(productID);
                    Orderdetail neworderDetail = new Orderdetail()
                    {
                        Quantity = newQuantity,
                        ProductID = productID,

                    };
                    newOrder.Add(neworderDetail);

                }

                var Address = Global.Address;
                var City = Global.City;
                var Country = Global.Country;
                var Phone = Global.Phone;

                OrderShippingAddress shippingAddress = new OrderShippingAddress()
                {
                    Address = Address,
                    City = City,
                    Country = "Nigeria",
                    PhoneNo = Phone,
                    Name = ""
                };

                Orderdetail orderdetail = new Orderdetail() { ProductID = "8354354347", Quantity = 222 };

                var walletID = Global.WalletId;

                PlaceOrderRequestModel requestPayload = new PlaceOrderRequestModel()
                { orderdetails = newOrder, DeliveryInstruction = DeliveryInstruction, ShippingAddress = shippingAddress, WalletID = walletID };

                string payloadJson = JsonConvert.SerializeObject(requestPayload);
                Console.WriteLine(payloadJson);

                string url = Global.PlaceOrderUrl;

                Console.WriteLine(url);
                StringContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response;
                response = await client.PostAsync(url, content);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await MessagePopup.Instance.Show("Order placed Successfully.");

                    Application.Current.MainPage = new NavigationPage(new Tabbed());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await MessagePopup.Instance.Show("Session expired.");

                    Application.Current.MainPage = new NavigationPage(new LoginPage());

                }
                else
                {
                    await MessagePopup.Instance.Show("Insufficient wallet balance.");
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
