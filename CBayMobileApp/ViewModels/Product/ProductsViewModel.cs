using CBayMobileApp.Models.Home;
using CBayMobileApp.Models.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Product
{

    public class ProductsViewModel : BaseViewModel
    {
        private ObservableCollection<ProductModel> cbayProducts;
        public ObservableCollection<ProductModel> CbayProducts
        {
            get => cbayProducts;
            set
            {
                cbayProducts = value;
                OnPropertyChanged(nameof(CbayProducts));
            }
        }

        public ProductsViewModel(INavigation navigation)
        {
            Navigation = navigation;

            CbayProducts = new ObservableCollection<ProductModel>{
                new ProductModel { amount = "NGN49", PlusImage = "cbayproduct1.png", title = "Adidas Sport Tights" },
                new ProductModel { amount = "NGN49", PlusImage = "cbayproducts3.png", title = "Adidas Sport Tights" },
                new ProductModel { amount = "NGN49", PlusImage = "cbayproduct2.png", title = "Adidas Sport Tights" },
                new ProductModel { amount = "NGN49", PlusImage = "cbayproduct1.png", title = "Adidas Sport Tights" },
                new ProductModel { amount = "NGN49", PlusImage = "cbayproduct2.png", title = "Adidas Sport Tights" },
                new ProductModel { amount = "NGN49", PlusImage = "cbayproducts3.png", title = "Adidas Sport Tights" },
                

             };
        }

    }

}
