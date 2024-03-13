using CBayMobileApp.Helpers;
using CBayMobileApp.Views.Identity;
using CBayMobileApp.Views.Onboarding;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPopup : PopupPage
    {
        public LogoutPopup()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Close_Popup(object sender, EventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
        }

        [Obsolete]
        private async void Confirm(object sender, EventArgs e)
        {
            Global.token = null;
            await PopupNavigation.RemovePageAsync(this);
            Application.Current.MainPage = new NavigationPage(new LoginPage());

        }
    }
}