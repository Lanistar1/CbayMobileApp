using CBayMobileApp.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentEarning : ContentPage
    {
        public RecentEarning()
        {
            InitializeComponent();
            BindingContext = new RecentEarningViewModel(Navigation);
        }
    }
}