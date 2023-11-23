using CBayMobileApp.Models.Membership;
using CBayMobileApp.ViewModels.Membership;
using CBayMobileApp.ViewModels;
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
    public partial class TeamDownline : ContentPage
    {
        GetMembershipTeamDownlineViewModel pageViewModel = null;

        public TeamDownline(List<DownlineData> SelectedItems)
        {
            pageViewModel = new GetMembershipTeamDownlineViewModel(Navigation, SelectedItems);
            InitializeComponent();
            BindingContext = pageViewModel;
        }
    }
}