using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using CBayMobileApp.Views.Onboarding;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("DMSans_18pt-Black.ttf", Alias = "DMSans-Black")]
[assembly: ExportFont("DMSans_18pt-Bold.ttf", Alias = "DMSans-Bold")]
[assembly: ExportFont("DMSans_18pt-Light.ttf", Alias = "DMSans-Light")]
[assembly: ExportFont("DMSans_18pt-Medium.ttf", Alias = "DMSans-Medium")]
[assembly: ExportFont("DMSans_18pt-Regular.ttf", Alias = "DMSans-Regular")]

namespace CBayMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            MainPage = new NavigationPage(new Onboard());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
