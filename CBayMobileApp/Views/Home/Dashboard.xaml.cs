using CBayMobileApp.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new TransactionViewModel(Navigation);
        }

        private void To_recentEarning(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecentEarning());
        }

        private void To_addMoney(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddMoney());
        }
    }
}