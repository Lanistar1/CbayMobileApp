using CBayMobileApp.Helpers;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoaderPop : PopupPage
    {
        public static bool IsLoading { get; private set; }
        public static bool IsAppearing { get; private set; }
        public LoaderPop()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            IsLoading = false;
        }

        #region Instance

        private static LoaderPop _instance;

        public static LoaderPop Instance => _instance ?? (_instance = new LoaderPop());

        public async Task Show(string _topLabel = null, string _bottomLabel = null,
            object hideCommandParameter = null, bool isHasBottomButton = false)
        {
            IsAppearing = true;

            await DeviceExtension.BeginInvokeOnMainThreadAsync(() =>
            {
                topLabel.Text = !string.IsNullOrWhiteSpace(_topLabel)
                    ? _topLabel
                    : "Please";
                bottomLabel.Text = !string.IsNullOrWhiteSpace(_bottomLabel)
                    ? _bottomLabel
                    : "wait..";

            });

            if (IsLoading) return;

            IsLoading = true;

            await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(this);
            });

            IsAppearing = false;
        }

        #endregion

        #region StopProcessing

        public async Task Hide()
        {
            // Waiting for Loading Popup start appearing
            await Task.Delay(50);

            if (IsLoading)
            {
                await Task.Delay(200);

                if (IsAppearing)
                {
                    // Wait for Loading Popup finish appearing
                    await Task.Delay(200);
                }

                IsLoading = false;

                if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count != 0)
                    await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                    {
                        await Navigation.PopPopupAsync();
                    });
            }
        }

        #endregion

        // Lock hard ware back button
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        #region RefreshUI

        public void RefreshUI()
        {
            InitializeComponent();
        }

        #endregion
    }

}