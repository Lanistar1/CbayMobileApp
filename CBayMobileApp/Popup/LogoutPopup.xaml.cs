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

        private void Close_Popup(object sender, EventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
        }

        [Obsolete]
        private async void Confirm(object sender, EventArgs e)
        {
            //Global.Token = null;
            await PopupNavigation.RemovePageAsync(this);
            //Application.Current.MainPage = new Login();

        }
    }
}