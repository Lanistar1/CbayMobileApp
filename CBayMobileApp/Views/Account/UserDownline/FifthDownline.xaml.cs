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
    public partial class FifthDownline : ContentPage
    {
        FifthDownlineViewModel pageViewModel = null;

        public FifthDownline(List<DownlineData> SelectedItems)
        {
            pageViewModel = new FifthDownlineViewModel(Navigation, SelectedItems);
            InitializeComponent();
            BindingContext = pageViewModel;
        }
    }
}