using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.AuthFlow
{

    public class ForgotPasswordViewModel : BaseViewModel
    {
        public ForgotPasswordViewModel(INavigation navigation)
        {
            Navigation = navigation;

            ForgotPasswordCommand = new Command(async () => await ForgotPasswordCommandExecute(UserName));
        }


        #region Bindings
        private string token;
        public string Token
        {
            get => token;
            set
            {
                token = value;
                OnPropertyChanged(nameof(Token));
            }
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }


        #endregion


        #region Commands
        public Command ForgotPasswordCommand { get; }

        #endregion


        #region functions, methods, events and Navigations

        private async Task ForgotPasswordCommandExecute(string UserName)
        {
            try
            {

                await LoadingPopup.Instance.Show("Verifying user...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.ResetUserPasswordAsync(UserName);


                if (ResponseData != null)
                {
                    await MessagePopup.Instance.Show("Password reset link has been sent to your mail.");

                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else if (ErrorData != null && StatusCode == 400)
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
                else if (ErrorData != null && StatusCode == 500)
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
                else
                {
                    await MessagePopup.Instance.Show("Password reset link has been sent to your mail.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await MessagePopup.Instance.Show("Something went wrong. Please try again later.");
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }

        }

        #endregion
    }

}
