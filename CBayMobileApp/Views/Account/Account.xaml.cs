using CBayMobileApp.ViewModels.AuthFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : ContentPage
    {
        GetProfileViewModel pageViewModel = null;

        public Account()
        {
            pageViewModel = new GetProfileViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_Membership(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Membership());
        }

        private void To_UserProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProfile());
        }
    }
}