using CBayMobileApp.Helpers;
using CBayMobileApp.ViewModels.AuthFlow;
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
    public partial class EditProfile : ContentPage
    {
        private string genderType;
        private DateTime DateType { get; set; }

        EditProfileViewModel pageViewModel = null;

        public EditProfile()
        {
            pageViewModel = new EditProfileViewModel(Navigation);
            InitializeComponent();
            BindingContext = pageViewModel;
        }

        private void GenderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;

            genderType = picker.SelectedItem.ToString();

            Console.WriteLine(genderType);

            Global.GenderType = genderType;
        }

        private void DatePicker_SelectedDateChanged(object sender, DateChangedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;

            DateType = picker.Date;

            Console.WriteLine(DateType);

            Global.DateType = DateType;
        }
    }
}