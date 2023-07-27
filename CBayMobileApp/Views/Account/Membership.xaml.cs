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
    public partial class Membership : ContentPage
    {
        public Membership()
        {
            InitializeComponent();
        }

        private void To_Downlines(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Downlines());
        }

        private void To_teamDownlines(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TeamDownline());
        }
    }
}