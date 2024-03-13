using CBayMobileApp.Helpers;
using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Shopping;
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

        private List<WalletData> walletData;
        public List<WalletData> WalletData
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

        private bool walletBonus;
        public bool WalletBonus
        {
            get => walletBonus;
            set
            {
                walletBonus = value;
                OnPropertyChanged(nameof(WalletBonus));
            }
        }

        private int walletNo;
        public int WalletNo
        {
            get => walletNo;
            set
            {
                walletNo = value;
                OnPropertyChanged(nameof(WalletNo));
            }
        }

        private string balance;
        public string Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

        private string bonusBalance;
        public string BonusBalance
        {
            get => bonusBalance;
            set
            {
                bonusBalance = value;
                OnPropertyChanged(nameof(BonusBalance));
            }
        }

        private string availableBalance;
        public string AvailableBalance
        {
            get => availableBalance;
            set
            {
                availableBalance = value;
                OnPropertyChanged(nameof(AvailableBalance));
            }
        }

        private List<GetAllProductData> productData;
        public List<GetAllProductData> ProductData
        {
            get => productData;
            set
            {
                productData = value;
                OnPropertyChanged(nameof(ProductData));
            }
        }

        #endregion

        #region functions, methods, navigations, events
        private async Task UpdatePageBindings()
        {
            await FetchWalletDetailAsync();
            await FetchWalletTransactionAsync();
            await FetchUserProfile();
            await FetchAllProductsAsync();


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
                    //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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

                        //AvailableBalance = WalletData.FirstOrDefault().balance;

                        List<string> detailLot = new List<string>();

                        foreach (var item in WalletData)
                        {
                            AvailableBalance = item.DisplayAmount;

                            detailLot.Add(AvailableBalance);

                            if (item.isCompensation == true)
                            {
                                WalletBonus = true;
                            }
                            else
                            {
                                WalletBonus = false;
                            }
                        }

                        Balance = walletData.FirstOrDefault(x=>x.isCompensation == false).DisplayAmount;
                        BonusBalance = walletData.FirstOrDefault(x=>x.isCompensation == true).DisplayAmount;
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


        private async Task FetchAllProductsAsync()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Products...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetAllProductAsync();
                if (ResponseData != null)
                {
                    if (ResponseData != null)
                    {
                        ProductData = ResponseData.data;
                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching products detail. Do you want to RETRY?";
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
                string message = "Error fetching products detail. Do you want to RETRY?";
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
