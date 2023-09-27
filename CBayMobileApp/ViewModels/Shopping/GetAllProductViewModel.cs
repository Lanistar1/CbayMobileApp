using CBayMobileApp.Models.Shopping;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #endregion

        #region Commands
        public Command PlaceOrderCommand { get; }
        public Command TappedCommand { get; }

        #endregion


        #region functions, methods, events and Navigations

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
