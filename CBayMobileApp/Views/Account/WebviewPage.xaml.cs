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
    public partial class WebviewPage : ContentPage
    {
        public WebviewPage(string url)
        {
            InitializeComponent();
            webView.Source = new UrlWebViewSource { Url = url };

        }
    }
}