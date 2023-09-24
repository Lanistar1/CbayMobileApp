using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
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
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel(INavigation navigation)
        {
            Navigation = navigation;

            RegisterCommand = new Command(async () => await RegisterCommandsExecute(username, password, refCode));
        }

        #region Bindings
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


        private string refCode;
        public string RefCode
        {
            get => refCode;
            set
            {
                refCode = value;
                OnPropertyChanged(nameof(RefCode));
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

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
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
        public Command RegisterCommand { get; }
        #endregion

        #region functions, methods, navigations, events
        private async Task RegisterCommandsExecute(string username, string password, string refCode)
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

            if (ConfirmPassword != Password)
            {
                await MessagePopup.Instance.Show("Password and confirm password are not the same. ");

                return;
            }


            try
            {

                await LoadingPopup.Instance.Show("Registering user...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.SignupUserAsync(username, password, refCode);


                if (ResponseData != null)
                {
                    await MessagePopup.Instance.Show("Registration Successful.");

                    Global.token = ResponseData.token;

                    Console.WriteLine(ResponseData.token);

                    await Navigation.PushAsync(new UpdateProfile());

                    //Application.Current.MainPage = new NavigationPage(new RegisterOption());
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
