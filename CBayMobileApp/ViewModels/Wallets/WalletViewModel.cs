using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Wallet;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private GetMyWalletResponseModel walletData;
        public GetMyWalletResponseModel WalletData
        {
            get => walletData;
            set
            {
                walletData = value;
                OnPropertyChanged(nameof(WalletData));
            }
        }

        #endregion

        #region functions, methods, navigations, events
        private async Task UpdatePageBindings()
        {
            await FetchWalletDetailAsync();
            await FetchWalletTransactionAsync();
            //await FetchRecentCollections(agentId);

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
                        WalletData = ResponseData;
                        Global.UserWalletData = WalletData;
                    }
                    else
                    {
                        await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    string message = "Error fetching user wallet detail. Please try again";
                    await MessagePopup.Instance.Show(
                        message: message);

                }
                else
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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


                    }
                    else
                    {
                        await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    string message = "Error fetching wallet transaction. Please try again";
                    await MessagePopup.Instance.Show(
                        message: message);

                }
                else
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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
