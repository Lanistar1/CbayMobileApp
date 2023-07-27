using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : ContentPage
    {
        public Account()
        {
            InitializeComponent();
        }

        private void To_Membership(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Membership());
        }
    }
}