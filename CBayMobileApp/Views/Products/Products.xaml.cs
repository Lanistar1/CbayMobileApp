using CBayMobileApp.ViewModels.Home;
using CBayMobileApp.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentPage
    {
        public Products()
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel(Navigation);
        }

        private void To_productdetails(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductDetail());
        }
    }
}