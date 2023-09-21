using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Shopping;
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

namespace CBayMobileApp.ViewModels.Shopping
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private string selectedItemCount = "0";
        public string SelectedItemCount
        {
            get => selectedItemCount;
            set
            {
                selectedItemCount = value;
                OnPropertyChanged(nameof(SelectedItemCount));
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

        private string productImage;
        public string ProductImage
        {
            get => productImage;
            set
            {
                productImage = value;
                OnPropertyChanged(nameof(ProductImage));
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

        private string vendorID;
        public string VendorID
        {
            get => vendorID;
            set
            {
                vendorID = value;
                OnPropertyChanged(nameof(VendorID));
            }
        }


        public List<GetAllProductData> selectedItems;
        public List<GetAllProductData> SelectedItems
        {
            get => selectedItems;
            set
            {
                selectedItems = value;
            }
        }

        public ProductDetailViewModel(INavigation navigation, List<GetAllProductData> selectedItems)
        {

            Navigation = navigation;

            SelectedItems = selectedItems;

            NextCommand = new Command<GetAllProductData>(async (model) => await NextCommandExecute(model));

            ProductDetail = new List<GetAllProductData>(SelectedItems);

            var articleUrl = ProductDetail;

            ProductImage = ProductDetail.FirstOrDefault().defaultPictureLocation;
            ProductName = ProductDetail.FirstOrDefault().name;
            ProductPrice = ProductDetail.FirstOrDefault().productPrice;
            VendorID = ProductDetail.FirstOrDefault().restuarantID;
            ProductID = ProductDetail.FirstOrDefault().productID;

            //Global.VendorID = vendorID;
        }

        public Command NextCommand { get; }
        public Command AddCartCommand { get; }
        public Command DeleteCommand { get; set; }

        
        private async Task NextCommandExecute(GetAllProductData model)
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

                //await Navigation.PushAsync(new OrderConfirmation(SelectedItems), true);
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
