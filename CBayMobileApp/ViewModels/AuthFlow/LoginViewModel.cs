using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.AuthFlow
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;

            LoginCommand = new Command(async () => await LoginCommandsExecute(username, password));
        }

        #region Binding Properties
        private bool isBtnEnabled = false;
        public bool IsBtnEnabled
        {
            get => isBtnEnabled;
            set
            {
                isBtnEnabled = value;
                OnPropertyChanged(nameof(IsBtnEnabled));
            }
        }


        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public bool ValidateEmail(string Username)
        {
            if (string.IsNullOrWhiteSpace(Username))
                return false;

            return EmailRegex.IsMatch(Username);
        }
        #endregion

        #region commands
        public Command LoginCommand { get; }
        #endregion


        #region functions, methods, navigations, events
        private async Task LoginCommandsExecute(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                await MessagePopup.Instance.Show("Email field should not be empty");

            }
            else
            {
                var x = EmailRegex.Match(Username);
                if (x.Success)
                {
                    // do something
                }
                else
                {

                    await MessagePopup.Instance.Show("Email field not correct. Field must contain @ and .com ");

                    return;
                }
            }


            if (string.IsNullOrWhiteSpace(Password))
            {
                await MessagePopup.Instance.Show("Password field should not be empty");

                return;
            }
            try
            {

                await LoadingPopup.Instance.Show("Logging In...");

                Global.UserEmail = Username;

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.LoginUserAsync(username, password);


                if (ResponseData != null)
                {
                    Global.token = ResponseData.token;

                    Console.WriteLine(ResponseData.token);
                    if (ResponseData.token == "2FA")
                    {

                        await Navigation.PushAsync(new OTPVerification());
                    }
                    else
                    {
                        await MessagePopup.Instance.Show("Login Successful.");

                        Application.Current.MainPage = new NavigationPage(new Tabbed());

                    }

                }
                else if (ErrorData != null && StatusCode == 401)
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
                else
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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
