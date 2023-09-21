using CBayMobileApp.Views.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Onboard : ContentPage
    {
        public Onboard()
        {
            InitializeComponent();
        }

        private void To_Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void To_Signup(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }
    }
}