using CBayMobileApp.ViewModels.AuthFlow;
using CBayMobileApp.ViewModels;
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
    public partial class ForgotPasswordPage : ContentPage
    {
        ForgotPasswordViewModel pageViewModel = null;

        public ForgotPasswordPage()
        {
            pageViewModel = new ForgotPasswordViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}