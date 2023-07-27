using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBayMobileApp.Utils
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmPopup : PopupBasePage
    {
        public ConfirmPopup()
        {
            InitializeComponent();
        }

        #region Instance

        private static ConfirmPopup _instance;

        //this method is known as compound assignment
        public static ConfirmPopup Instance => _instance = new ConfirmPopup() { IsClosed = true };

        //public static ConfirmPopup Instance => _instance ??= new ConfirmPopup() { IsClosed = true };

        public async Task<ConfirmPopup> Show(string title = null, string message = null, string closeButtonText = null,
            ICommand closeCommand = null, object closeCommandParameter = null,
            string acceptButtonText = null, ICommand acceptCommand = null, object acceptCommandParameter = null,
            bool isAutoClose = false, uint duration = 2000, string rightCornerIcon = null, string backgroundColor = null)
        {
            // Close Loading Popup if it is showing
            await LoadingPopup.Instance.Hide();
            await LoaderPop.Instance.Hide();

            await DeviceExtension.BeginInvokeOnMainThreadAsync(() =>
            {
                if (title != null)
                    LabelTitle.Text = title;
                else
                    LabelTitle.Text = "Confirm";

                if (message != null)
                    LabelMessage.Text = message;
                else
                    LabelMessage.Text = "";

                if (closeButtonText != null)
                    ButtonClose.Text = closeButtonText;
                else
                    ButtonClose.Text = "Cancel";

                ClosePopupCommand = closeCommand;
                ClosePopupCommandParameter = closeCommandParameter;

                if (acceptButtonText != null)
                    ButtonAccept.Text = acceptButtonText;
                else
                    ButtonAccept.Text = "OK";

                AcceptCommand = acceptCommand;
                AcceptCommandParameter = acceptCommandParameter;

                IsAutoClose = isAutoClose;
                Duration = duration;

                RightCornerIcon.Source = string.IsNullOrWhiteSpace(rightCornerIcon) ? "alertIcon.png" : rightCornerIcon;

                GridHasButton.BackgroundColor =
                    string.IsNullOrWhiteSpace(backgroundColor) ? Color.FromHex("0088C3") : Color.FromHex(backgroundColor);

            });

            if (IsClosed)
            {
                IsClosed = false;

                if (isAutoClose && duration > 0)
                    AutoClosePopupAfter(duration);

                await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(this);
                });
            }

            return this;
        }

        #endregion

        #region Events
        private async void AcceptPopupEvent(object sender, EventArgs e)
        {
            await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
            {
                await Navigation.PopPopupAsync();
            });

            // waiting for close animation finished
            await Task.Delay(300);

            AcceptCommand?.Execute(AcceptCommandParameter);

            //_popupId++;
            IsClosed = true;
        }

        #endregion

        #region AcceptCommand

        public static readonly BindableProperty AcceptCommandProperty =
            BindableProperty.Create(nameof(AcceptCommand),
                typeof(ICommand),
                typeof(ConfirmPopup),
                null,
                BindingMode.TwoWay);

        public ICommand AcceptCommand
        {
            get => (ICommand)GetValue(AcceptCommandProperty);
            set => SetValue(AcceptCommandProperty, value);
        }

        public static readonly BindableProperty AcceptCommandParameterProperty =
            BindableProperty.Create(nameof(AcceptCommandParameter),
                typeof(object),
                typeof(ConfirmPopup),
                null,
                BindingMode.TwoWay);

        public object AcceptCommandParameter
        {
            get => GetValue(AcceptCommandParameterProperty);
            set => SetValue(AcceptCommandParameterProperty, value);
        }

        #endregion

        #region RefreshUI

        public void RefreshUI()
        {
            InitializeComponent();
        }

        #endregion
    }

}