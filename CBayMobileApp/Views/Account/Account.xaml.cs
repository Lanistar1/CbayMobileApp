using CBayMobileApp.Popup;
using CBayMobileApp.ViewModels.AuthFlow;
using CBayMobileApp.Views.Order;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        private async void To_Membership(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Membership());
        }

        private async void To_UserProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfile());
        }

        private async void To_Order(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyOrder());

        }

        private async void To_inviteFriend(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InviteFriend());
        }

        private async void To_privacypolicy(object sender, EventArgs e)
        {
            string url = $"https://cbays.ng/policy";

            await Launcher.OpenAsync(url);
        }

        private async void To_terms(object sender, EventArgs e)
        {
            string url = $"https://cbays.ng/terms";

            await Launcher.OpenAsync(url);
        }

        private void To_logoutpopup(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new LogoutPopup());
        }
    }
}