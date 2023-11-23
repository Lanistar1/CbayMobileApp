using CBayMobileApp.ViewModels.AuthFlow;
using CBayMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CBayMobileApp.Views.Identity;

namespace CBayMobileApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        GetProfileViewModel pageViewModel = null;

        public UserProfile()
        {
            pageViewModel = new GetProfileViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void To_UpdateProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfile());
        }
    }
}