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
    public partial class SignupPage : ContentPage
    {
        RegisterViewModel pageViewModel = null;

        public SignupPage()
        {
            pageViewModel = new RegisterViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}