using CBayMobileApp.Models.Common;
using CBayMobileApp.ViewModels.Common;
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
    public partial class SelectItemPickerPopup : PopupPage
    {
        private TaskCompletionSource<Tuple<string>> _taskCompletionSource;
        public Task<Tuple<string>> PopupClosedTask => _taskCompletionSource.Task;
        public static SelectItemPickerPopup Instance { get; private set; }
        public SelectItemPickerPopupViewModel viewModel = null;
        public SelectItemPickerPopup(List<SelectItemModel> items)
        {
            viewModel = new SelectItemPickerPopupViewModel(Navigation, items);
            InitializeComponent();
            Instance = this;
            BindingContext = viewModel;

        }

        protected override bool OnBackButtonPressed()
        {
            HandleBackButtonPressed();
            return true;
        }

        private async void HandleBackButtonPressed()
        {
            await SelectItemPickerPopupViewModel.Instance.ClosePopUp(string.Empty);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            _taskCompletionSource = new TaskCompletionSource<Tuple<string>>();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _taskCompletionSource.SetResult(((SelectItemPickerPopupViewModel)BindingContext).ReturnValue);
        }

    }

}