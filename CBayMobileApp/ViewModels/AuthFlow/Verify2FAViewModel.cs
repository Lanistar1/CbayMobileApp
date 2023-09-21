using CBayMobileApp.Helpers;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.AuthFlow
{
    public class Verify2FAViewModel : BaseViewModel
    {
        public Verify2FAViewModel(INavigation navigation)
        {
            Navigation = navigation;



            Verify2FACommand = new Command(async () => await Verify2FACommandExecute(userName, token));
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

        private string userName = Global.UserEmail;
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
        public Command Verify2FACommand { get; }

        #endregion


        #region functions, methods, events and Navigations

        private async Task Verify2FACommandExecute(string userName, string token)
        {
            try
            {

                await LoadingPopup.Instance.Show("Verifying user...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.VerifyUserAsync(userName, token);


                if (ResponseData != null)
                {
                    await MessagePopup.Instance.Show("User verified Successfully.");

                    Global.token = ResponseData.token;

                    Console.WriteLine(ResponseData.token);


                    Application.Current.MainPage = new NavigationPage(new Tabbed());
                }

                else if (ErrorData != null && StatusCode == 400)
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
