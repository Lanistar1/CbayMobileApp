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
    public partial class InviteFriend : ContentPage
    {
        GetMembershipDetailViewModel pageViewModel = null;

        public InviteFriend()
        {
            pageViewModel = new GetMembershipDetailViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }
    }
}