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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel pageViewModel = null;

        public LoginPage()
        {
            pageViewModel = new LoginViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_ForgotPassword(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }

        private void To_Signup(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }

        private void To_Tabbed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Tabbed());
        }
    }
}