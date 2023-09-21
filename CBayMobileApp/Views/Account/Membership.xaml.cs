using CBayMobileApp.ViewModels.AuthFlow;
using CBayMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CBayMobileApp.ViewModels.Membership;

namespace CBayMobileApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Membership : ContentPage
    {
        GetMembershipDetailViewModel pageViewModel = null;

        public Membership()
        {
            pageViewModel = new GetMembershipDetailViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_Downlines(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Downlines());
        }

        private void To_teamDownlines(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TeamDownline());
        }
    }
}