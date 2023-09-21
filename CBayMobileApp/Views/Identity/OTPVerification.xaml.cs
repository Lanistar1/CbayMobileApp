using CBayMobileApp.ViewModels.AuthFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Identity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPVerification : ContentPage
    {
        Verify2FAViewModel pageViewModel = null;

        public OTPVerification()
        {
            pageViewModel = new Verify2FAViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private async void Back(object sender, EventArgs e)
        {
            int backCount = 1;
            for (var counter = 1; counter < backCount; counter++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
            await Navigation.PopAsync();
        }

        private void To_Tabbed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Tabbed());
        }
    }
}