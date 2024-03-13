using CBayMobileApp.ViewModels.Shopping;
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
    public partial class AddToCartPopup : PopupPage
    {
        public AddToCartPopup()
        {
            InitializeComponent();
            BindingContext = new CartViewModel(Navigation);

        }

        [Obsolete]
        private async void Cancel(object sender, EventArgs e)
        {
            await PopupNavigation.RemovePageAsync(this);

            //PopupNavigation.Instance.PopAsync();

        }
    }
}