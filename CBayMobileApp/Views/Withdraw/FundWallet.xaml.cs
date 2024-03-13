using CBayMobileApp.Helpers;
using CBayMobileApp.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using CBayMobileApp.Models.Wallet;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CBayMobileApp.Services;



namespace CBayMobileApp.Views.Withdraw
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FundWallet : ContentPage
    {
        public string firstName;
        public string email;
        public string phoneNumber;
        public string newAmount;

        public FundWallet()
        {
            InitializeComponent();
        }

        private async void To_flutterwave(object sender, EventArgs e)
        {
            var userData = Global.UserProfileData;
            firstName = userData.firstName;
            email = userData.emailAddress;
            newAmount = fundAmount.Text;
            phoneNumber = userData.phoneNo;

            string url = $"https://flutterwave.cbays.ng/?amount={newAmount}&name={firstName}&email={email}&phone={phoneNumber}"; // Replace with your desired URL
            wv.IsVisible = true;
            wv.Source = url;
            

            //await Launcher.OpenAsync(url);
        }

        private  async void G_Navigated(object sender, WebNavigatedEventArgs e)
        {
              CbayServices _cbayServices = new CbayServices();

            if (e.Url.Contains("success"))
            {
                try
                {
                    HttpClient client = new HttpClient();
                      
                    await LoadingPopup.Instance.Show("funding wallet");

                    var BankCode = Global.bankName;
                    if (Global.UserWalletData == null)
                    {
                        var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetWalletAsync();
                        if (ResponseData != null)
                        {
                            if (ResponseData != null)
                            {
                                
                                Global.UserWalletData = ResponseData.data;
                            }
                        }
                    }

                  if( Global.UserWalletData != null)
                    {

                   

                    double newAmount = double.Parse(fundAmount.Text);
                    var uri = new Uri(e.Url);

                   var qry = System.Web.HttpUtility.ParseQueryString(uri.Query);

                        var realWallet = Global.UserWalletData.FirstOrDefault(x => x.isCompensation == false);
                    FundWalletModel requestPayload = new FundWalletModel()
                    { FundTransRef = qry["transaction_id"], Amount = newAmount, WalletID = realWallet.walletID };

                    string payloadJson = JsonConvert.SerializeObject(requestPayload);

                    Console.WriteLine(payloadJson);

                    string url = Global.FundUrl;
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
                        await MessagePopup.Instance.Show("Wallet Funding Successfully.");
                        Application.Current.MainPage = new NavigationPage(new Tabbed());
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        Application.Current.MainPage = new NavigationPage(new LoginPage());

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await MessagePopup.Instance.Show("Wallet Funding Not Successful.");
                            wv.IsVisible = false;

                        }
                    else
                    {
                        await MessagePopup.Instance.Show("Something went wrong. Please try again later.");
                            wv.IsVisible = false;
                        }
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
            //else
            //{
            //    await DisplayAlert("Wallet Funding", "Funding Failed", "ok");
            //    wv.IsVisible = false;
            //}
        }
    }
}