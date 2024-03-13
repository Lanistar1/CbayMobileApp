using CBayMobileApp.Models.Shopping;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Order
{
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Task _tsk = UpdatePageBindings();

            RefreshCommand = new Command(async () => await RefreshCommandExecute());

        }


        #region Bindings

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

        private List<GetMyOrderModelData> getAllOrder;
        public List<GetMyOrderModelData> GetAllOrder
        {
            get => getAllOrder;
            set
            {
                getAllOrder = value;
                OnPropertyChanged(nameof(GetAllOrder));
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

        private ShippingAddressData userAddressData;
        public ShippingAddressData UserAddressData
        {
            get => userAddressData;
            set
            {
                userAddressData = value;
                OnPropertyChanged(nameof(UserAddressData));
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

        #endregion

        #region Commands
        public Command PlaceOrderCommand { get; }
        public Command RefreshCommand { get; }

        #endregion


        #region functions, methods, events and Navigations

        private async Task UpdatePageBindings()
        {
            await FetchOrderDetailAsync();

        }

        public async Task RefreshCommandExecute()
        {
            await FetchOrderDetailAsync();
            // Stop refreshing
            IsRefreshing = false;
        }

        private async Task FetchOrderDetailAsync()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Order detail...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetMyOrderAsync();
                if (ResponseData != null)
                {
                    GetAllOrder = ResponseData.data;

                    //UserOrder = ResponseData.orderDetails;

                    //foreach (var item in UserOrder)
                    //{
                    //    ProductName = item.productName;
                    //    ProductPrice = item.unitPrice;
                    //}

                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching user order detail. Do you want to RETRY?";
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
                string message = "Error fetching user order detail. Do you want to RETRY?";
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
