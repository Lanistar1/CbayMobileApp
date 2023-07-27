using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CBayMobileApp.Helpers
{
    public class PopupBasePage : PopupPage
    {
        public PopupBasePage()
        { }

        #region internal variables

        protected bool IsAutoClose;
        public bool IsClosed { get; protected set; }
        protected uint Duration;
        protected int _popupId;

        #endregion

        #region Page Event
        protected async void ClosePopupEvent(object sender, EventArgs e)
        {
            await ClosePopup();
        }

        protected async void OkPopupEvent(object sender, EventArgs e)
        {
            await ClosePopup();
        }


        protected void DoNothingEvent(object sender, EventArgs e)
        {
            // Do nothing
        }

        protected async void AutoClosePopupAfter(uint duration)
        {
            int id = _popupId;
            await Task.Delay((int)duration);

            // If the popup still appear after duration time then close the popup
            if (_popupId == id)
                await ClosePopup();
        }

        public async Task ClosePopup()
        {
            if (IsClosed)
                return;
            await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
            {
                Debug.WriteLine("Close message popup");
                await PopupNavigation.Instance.PopAsync();
            });

            ClosePopupCommand?.Execute(ClosePopupCommandParameter);
            _popupId++;

            IsClosed = true;
        }
        #endregion

        #region ClosePopupCommand

        public static readonly BindableProperty ClosePopupCommandProperty =
            BindableProperty.Create(nameof(ClosePopupCommand),
                typeof(ICommand),
                typeof(PopupBasePage),
                null,
                BindingMode.TwoWay);

        public ICommand ClosePopupCommand
        {
            get => (ICommand)GetValue(ClosePopupCommandProperty);
            set => SetValue(ClosePopupCommandProperty, value);
        }

        public static readonly BindableProperty ClosePopupCommandParameterProperty =
            BindableProperty.Create(nameof(ClosePopupCommandParameter),
                typeof(object),
                typeof(PopupBasePage),
                null,
                BindingMode.TwoWay);

        public object ClosePopupCommandParameter
        {
            get => GetValue(ClosePopupCommandParameterProperty);
            set => SetValue(ClosePopupCommandParameterProperty, value);
        }

        #endregion

        protected override void OnDisappearing()
        {
            IsClosed = true;
            base.OnDisappearing();
        }
    }

}
