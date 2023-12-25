using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Wallet;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Wallets
{
    public class withdrawalViewModel : BaseViewModel
    {
        private List<BankData> bankList;
        public List<BankData> BankList
        {
            get => bankList;
            set
            {
                bankList = value;
                OnPropertyChanged(nameof(BankList));
            }
        }


        private string approval;
        public string Approval
        {
            get => approval;
            set
            {
                approval = value;
                OnPropertyChanged(nameof(Approval));
            }
        }

        private string currencyType;
        public string CurrencyType
        {
            get => currencyType;
            set
            {
                currencyType = value;
                OnPropertyChanged(nameof(CurrencyType));
            }
        }


        public withdrawalViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Task _task = GetMyWalletExecute();
            Task _tasks = GetBankExecute();

            WithdrawalCommand = new Command(async () => await WithdrawalCommandExecute());

            //ValidateAccountCommand = new Command(async () => await ValidateAccountCommandExecute());


        }

        public Command WithdrawalCommand { get; }
        public Command FilterCommand { get; }
        public Command ValidateAccountCommand { get; }

        public Command AcctNumberTextChangedCommand => new Command<string>(async (_acctnumber) => await AcctNumberTextChanged(_acctnumber));


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

        private string walletID;
        public string WalletID
        {
            get => walletID;
            set
            {
                walletID = value;
                OnPropertyChanged(nameof(WalletID));
            }
        }

        private bool userDetailVisible = false;
        public bool UserDetailVisible
        {
            get => userDetailVisible;
            set
            {
                userDetailVisible = value;
                OnPropertyChanged(nameof(UserDetailVisible));
            }
        }

        private string bank;
        public string Bank
        {
            get => bank;
            set
            {
                bank = value;
                OnPropertyChanged(nameof(Bank));
            }
        }

        private string accountNumber;
        public string AccountNumber
        {
            get => accountNumber;
            set
            {
                accountNumber = value;
                AcctNumberTextChangedCommand.Execute(accountNumber);
                OnPropertyChanged(nameof(AccountNumber));
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

        private string amount;
        public string Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private string wallet;
        public string Wallet
        {
            get => wallet;
            set
            {
                wallet = value;
                OnPropertyChanged(nameof(Wallet));
            }
        }

        private string newwalletNo;
        public string NewwalletNo
        {
            get => newwalletNo;
            set
            {
                newwalletNo = value;
                OnPropertyChanged(nameof(NewwalletNo));
            }
        }

        private string accName;
        public string AccName
        {
            get => accName;
            set
            {
                accName = value;
                OnPropertyChanged(nameof(AccName));
            }
        }

        private string bankName;
        public string BankName
        {
            get => bankName;
            set
            {
                bankName = value;
                OnPropertyChanged(nameof(BankName));
            }
        }

        private string userAcc;
        public string UserAcc
        {
            get => userAcc;
            set
            {
                userAcc = value;
                OnPropertyChanged(nameof(UserAcc));
            }
        }

        private int _limit = 10;

        private async Task AcctNumberTextChanged(string acctNumber)
        {
            if (acctNumber.Length == _limit)
            {
                //if (!IsSelectBeneficiaryActive)
                //{
                //    if (string.IsNullOrEmpty(SelectedBankCode))
                //    {
                //        AlertUserToSelectBank();
                //    }
                //    else
                //    {
                //        await ValidateAccountCommandExecute(acctNumber);
                //    }
                //}
                //else
                //{
                //    return;
                //}

                await ValidateAccountCommandExecute(acctNumber);

            }
            else
            {
                return;
            }
        }

        public async Task WithdrawalCommandExecute()
        {
            try
            {
                HttpClient client = new HttpClient();

                await LoadingPopup.Instance.Show("Withdrawing fund...");

                var BankCode = Global.bankName;
                var SelectedWalletID = Global.SelectedWallet;

                double newAmount = double.Parse(Amount);

                WithdrawalRequestModel requestPayload = new WithdrawalRequestModel()
                { AccountNo = AccountNumber, Amount = newAmount, BankCode = BankCode, FromWalletID = WalletID };

                string payloadJson = JsonConvert.SerializeObject(requestPayload);

                Console.WriteLine(payloadJson);

                string url = Global.WithdrawalUrl;
                Console.WriteLine(url);
                StringContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response;
                response = await client.PostAsync(url, content);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //await Application.Current.MainPage.DisplayAlert("Withdrawal Successfully", "", "OK");
                    await MessagePopup.Instance.Show("Withdrawal Successfully.");
                    Application.Current.MainPage = new NavigationPage(new Tabbed());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await MessagePopup.Instance.Show("Withdrawal not Successful.");

                }
                else
                {
                    await MessagePopup.Instance.Show("Something went wrong. Please try again later.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }

        public async Task GetBankExecute()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading bank details...");

                HttpClient client = new HttpClient();

                string url = Global.GetBankUrl;

                Console.WriteLine(url);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response = await client.GetAsync(url);

                response = await client.GetAsync(url);

                Console.WriteLine(response);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetAllBanksModel data = JsonConvert.DeserializeObject<GetAllBanksModel>(result);
                    // Console.WriteLine("passedjiojiojiojio");

                    BankList = data.data;


                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    Console.WriteLine("Someting went wrong");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }


        private async Task GetMyWalletExecute()
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

                        WalletID = WalletData.FirstOrDefault().walletID;
                        WalletNo = WalletData.FirstOrDefault().walletNo;

                        //Global.UserWalletData = WalletData;
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

        public async Task ValidateAccountCommandExecute(string acctNumber)
        {
            try
            {
                HttpClient client = new HttpClient();

                await LoadingPopup.Instance.Show("Fetching account details...");

                var BankCode = Global.bankName;
                var SelectedWalletID = Global.SelectedWallet;

                ValidateAccountRequestModel requestPayload = new ValidateAccountRequestModel()
                { AccountNo = acctNumber, BankCode = BankCode };

                string payloadJson = JsonConvert.SerializeObject(requestPayload);

                Console.WriteLine(payloadJson);

                string url = Global.ValidateAccountUrl;
                Console.WriteLine(url);
                StringContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                HttpResponseMessage response;
                response = await client.PostAsync(url, content);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ValidateAccountResponseModel data = JsonConvert.DeserializeObject<ValidateAccountResponseModel>(result);

                    UserDetailVisible = true;

                    AccName = data.data.account_name;
                    BankName = Global.bank;
                    UserAcc = AccountNumber;
                    //await Navigation.PushAsync(new Assessment());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await Application.Current.MainPage.DisplayAlert("Account number does not match selected bank", "", "OK");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Something went wrong", "Please try again later", "OK");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }


    }

}
