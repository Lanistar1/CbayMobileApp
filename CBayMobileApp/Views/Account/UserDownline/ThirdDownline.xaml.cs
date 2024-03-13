using CBayMobileApp.ViewModels.Membership;
using CBayMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CBayMobileApp.Models.Membership;

namespace CBayMobileApp.Views.Account.UserDownline
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThirdDownline : ContentPage
    {
        ThirdDownlineViewModel pageViewModel = null;

        public ThirdDownline(List<DownlineData> SelectedItems)
        {
            pageViewModel = new ThirdDownlineViewModel(Navigation, SelectedItems);
            InitializeComponent();
            BindingContext = pageViewModel;
        }
    }
}