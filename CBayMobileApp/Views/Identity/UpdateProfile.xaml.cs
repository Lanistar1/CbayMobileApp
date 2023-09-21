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
    public partial class UpdateProfile : ContentPage
    {
        UpdateProfileViewModel pageViewModel = null;

        public UpdateProfile()
        {
            pageViewModel = new UpdateProfileViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }
    }
}