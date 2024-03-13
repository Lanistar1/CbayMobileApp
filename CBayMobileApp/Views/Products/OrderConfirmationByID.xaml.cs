using CBayMobileApp.ViewModels.Shopping;
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
    public partial class OrderConfirmationByID : ContentPage
    {
        public OrderConfirmationByID(BuyerProductByIDViewModel mod)
        {
            InitializeComponent();
            BindingContext = new OrderConfirmationByIDViewModel(Navigation, mod);

        }
    }
}