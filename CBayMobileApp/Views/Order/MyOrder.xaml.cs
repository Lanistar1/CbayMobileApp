using CBayMobileApp.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrder : ContentPage
    {
        public MyOrder()
        {
            InitializeComponent();
            BindingContext = new OrderViewModel(Navigation);

        }
    }
}