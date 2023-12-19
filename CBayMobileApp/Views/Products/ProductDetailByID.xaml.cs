using CBayMobileApp.Popup;
using CBayMobileApp.ViewModels.Shopping;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static CBayMobileApp.Helpers.Global;

namespace CBayMobileApp.Views.Products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailByID : ContentPage
    {
        private BuyerProductByIDViewModel ct;

        public ProductDetailByID(string ProductID)
        {
            InitializeComponent();

            ct = new BuyerProductByIDViewModel(Navigation, ProductID);
            BindingContext = ct;
        }

        private async void ConfirmOrder(object sender, EventArgs e)
        {

            try
            {
                await LoadingPopup.Instance.Show("Loading...");

                var mod = ct;

                //await Navigation.PushAsync(new OrderConfirmationByID(mod), true);
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

        private async void AddToCart(object sender, EventArgs e)
        {
            if (Helpers.Global.myCarts == null) { Helpers.Global.myCarts = new ObservableCollection<CartItem>(); }
            Helpers.Global.myCarts.Add(new CartItem
            {
                productID = ct.ProductByID.productID,
                quantity = 1,
                productName = ct.ProductByID.name,
                defaultPictureLocation = ct.ProductByID.defaultPictureLocation,
                productPrice = ct.ProductByID.productPrice
            });
            Console.WriteLine("kcsgcahjgha");

            await PopupNavigation.Instance.PushAsync(new AddToCartPopup());

        }


    }
}