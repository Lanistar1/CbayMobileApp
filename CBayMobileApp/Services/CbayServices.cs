using CBayMobileApp.Converters;
using CBayMobileApp.Helpers;
using CBayMobileApp.Models;
using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Membership;
using CBayMobileApp.Models.Shopping;
using CBayMobileApp.Models.Wallet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CBayMobileApp.Services
{

    public class CbayServices
    {
        HttpClient client;

        //ProfileData userProfileDetails = Global.UserProfileData == null ? null : Global.UserProfileData;
        //string token = Global.token;

        public async Task<(RegisterResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> SignupUserAsync(string username, string password, String refCode)
        {
            try
            {
                string url = Global.RegisterUrl;

                var RegisterData = new RegisterRequestModel
                {
                    Password = password,
                    Username = username, 
                    RefCode = refCode
                };
                var json = JsonConvert.SerializeObject(RegisterData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                ErrorResponseModel errorData;

                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, content);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        RegisterResponseModel data = JsonConvert.DeserializeObject<RegisterResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, statusCode);
                    default:
                        return (null, null, statusCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }

        }

        public async Task<(LoginResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> LoginUserAsync(string username, string password)
        {
            try
            {
                string url = Global.LoginUrl;

                var loginData = new LoginRequestModel
                {
                    Username = username,
                    Password = password
                };
                var json = JsonConvert.SerializeObject(loginData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                ErrorResponseModel errorData;

                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, content);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        LoginResponseModel data = JsonConvert.DeserializeObject<LoginResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, statusCode);
                    default:
                        return (null, null, statusCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }

        }

        public async Task<(Verify2FAResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> VerifyUserAsync(string userName, string token)
        {
            try
            {
                string url = Global.Verify2FAUrl;

                var Verify2FAData = new Verify2FARequestModel
                {
                    Username = userName,
                    Token = token
                };
                var json = JsonConvert.SerializeObject(Verify2FAData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                ErrorResponseModel errorData;

                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, content);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        Verify2FAResponseModel data = JsonConvert.DeserializeObject<Verify2FAResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, statusCode);
                    default:
                        return (null, null, statusCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }

        }

        public async Task<(GetProfileResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetUserProfileAsync()
        {
            try
            {
                string url = Global.GetProfileUrl;
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                //client.DefaultRequestHeaders.Add("Authorization", $"{token}");
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        var data = JsonConvert.DeserializeObject<GetProfileResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }

        public async Task<(UpdateProfileResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> UpdateUserProfileAsync(UpdateProfileRequestModel request)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = Global.UpdateUserProfileUrl;
                var json = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                ErrorResponseModel errorData;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");

                var response = await client.PutAsync(url, content);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        UpdateProfileResponseModel data = JsonConvert.DeserializeObject<UpdateProfileResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, statusCode);
                    default:
                        return (null, null, statusCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }

        }

        public async Task<(GetMyWalletResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetWalletAsync()
        {
            try
            {
                string url = Global.WalletUrl;
                HttpClient client = new HttpClient();

                //client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        var data = JsonConvert.DeserializeObject<GetMyWalletResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }

        public async Task<(GetWalletTransactionResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetWalletTransactionAsync()
        {
            try
            {
                string url = Global.WalletTransactionUrl;
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        GetWalletTransactionResponseModel data = JsonConvert.DeserializeObject<GetWalletTransactionResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }

        // direct member downline first level
        public async Task<(GetMembershipDownlineModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetMembershipAsync()
        {
            try
            {
                string url = Global.GetMembershipDownlineUrl;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        var data = JsonConvert.DeserializeObject<GetMembershipDownlineModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }


        // direct member downline second level
        public async Task<(GetMembershipDownlineModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetMembershipDownlineAsync(string MemberID)
        {
            try
            {
                string url = $"{Global.GetMembershipSecondDownlineUrl}/{MemberID}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        var data = JsonConvert.DeserializeObject<GetMembershipDownlineModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }


        public async Task<(MembershipDetailModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetMembershipDetailAsync()
        {
            try
            {
                string url = Global.GetMembershipDetailUrl;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        var data = JsonConvert.DeserializeObject<MembershipDetailModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }


        public async Task<(GetallProductsModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetAllProductAsync()
        {
            try
            {
                string url = Global.GetProductsUrl;

                HttpClient client = new HttpClient();

                //client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        var data = JsonConvert.DeserializeObject<GetallProductsModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }


        public async Task<(GetMyOrderModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> GetMyOrderAsync()
        {
            try
            {
                string url = Global.MyOrderUrl;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Helpers.Global.token}");
                HttpResponseMessage response = null;
                ErrorResponseModel errorData;
                response = await client.GetAsync(url);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:

                        var data = JsonConvert.DeserializeObject<GetMyOrderModel>(result);
                        return (data, null, statusCode);

                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, 0);
                    default:
                        return (null, null, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }
        }

        // Forgot password

        public async Task<(UpdateProfileResponseModel ResponseData, ErrorResponseModel ErrorData, int StatusCode)> ResetUserPasswordAsync(string UserName)
        {
            try
            {
                string url = Global.ForgotPasswordUrl;

                var Verify2FAData = new ForgotPasswordRequestModel
                {
                    Username = UserName
                };
                var json = JsonConvert.SerializeObject(Verify2FAData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                ErrorResponseModel errorData;

                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, content);
                int statusCode = (int)response.StatusCode;
                int _status = StringHelper.ConvertStatusCode((int)response.StatusCode);
                string result = await response.Content.ReadAsStringAsync();
                switch (_status)
                {
                    case 200:
                        UpdateProfileResponseModel data = JsonConvert.DeserializeObject<UpdateProfileResponseModel>(result);
                        return (data, null, statusCode);
                    case 300:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 400:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 500:
                        errorData = JsonConvert.DeserializeObject<ErrorResponseModel>(result);
                        return (null, errorData, statusCode);
                    case 0:
                        return (null, null, statusCode);
                    default:
                        return (null, null, statusCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, null, 0);
            }

        }

    }
}
