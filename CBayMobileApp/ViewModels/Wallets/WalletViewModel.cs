using CBayMobileApp.Helpers;
using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Wallet;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Wallets
{
    public class WalletViewModel : BaseViewModel
    {
        public WalletViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Task _tsk = UpdatePageBindings();

        }

        #region Binding Properties

        private List<GetWalletTransactionData> walletTransaction;
        public List<GetWalletTransactionData> WalletTransaction
        {
            get => walletTransaction;
            set
            {
                walletTransaction = value;
                OnPropertyChanged(nameof(WalletTransaction));
            }
        }

        private WalletData walletData;
        public WalletData WalletData
        {
            get => walletData;
            set
            {
                walletData = value;
                OnPropertyChanged(nameof(WalletData));
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string walletNo;
        public string WalletNo
        {
            get => walletNo;
            set
            {
                walletNo = value;
                OnPropertyChanged(nameof(WalletNo));
            }
        }

        private int availableBalance;
        public int AvailableBalance
        {
            get => availableBalance;
            set
            {
                availableBalance = value;
                OnPropertyChanged(nameof(AvailableBalance));
            }
        }
        #endregion

        #region functions, methods, navigations, events
        private async Task UpdatePageBindings()
        {
            await FetchWalletDetailAsync();
            await FetchWalletTransactionAsync();
            await FetchUserProfile();

        }

        private async Task FetchUserProfile()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Profile detail...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetUserProfileAsync();
                if (ResponseData != null)
                {
                    if (ResponseData.data != null)
                    {
                        
                        FirstName = ResponseData.data.firstName;
                       
                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching user detail. Do you want to RETRY?";
                    //await MessagePopup.Instance.Show(
                    //    message: message);

                }
                else
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                string message = "Error fetching user detail. Do you want to RETRY?";
                await MessagePopup.Instance.Show(
                    message: message);
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }


        private async Task FetchWalletDetailAsync()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Wallet detail...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetWalletAsync();
                if (ResponseData != null)
                {
                    if (ResponseData != null)
                    {
                        WalletData = ResponseData.data;
                        Global.UserWalletData = WalletData;

                        AvailableBalance = WalletData.balance;
                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching user wallet detail. Please try again";
                    //await MessagePopup.Instance.Show(
                    //    message: message);

                }
                else
                {
                    //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                string message = "Error fetching user wallet detail. Please try again";
                await MessagePopup.Instance.Show(
                    message: message);
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }

        private async Task FetchWalletTransactionAsync()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading wallet transaction...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetWalletTransactionAsync();
                if (ResponseData != null)
                {
                    if (ResponseData != null)
                    {
                        WalletTransaction = ResponseData.data;

                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching wallet transaction. Please try again";
                    //await MessagePopup.Instance.Show(
                    //    message: message);

                }
                else
                {
                    //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                string message = "Error fetching wallet transaction. Please try again";
                await MessagePopup.Instance.Show(
                    message: message);
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }


        #endregion
    }

}
