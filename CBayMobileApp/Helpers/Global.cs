using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Helpers
{
    public static class Global
    {
        public static string BaseUrl => "https://cbays-51894725732b.herokuapp.com/api/v1";
        public static string token { get; set; }
        public static string UserEmail { get; set; }
        public static string LoginUrl => $"{BaseUrl}/identity/login";
        public static string RegisterUrl => $"{BaseUrl}/identity/register";
        public static string Verify2FAUrl => $"{BaseUrl}/identity/token/verify";
        public static string GetProfileUrl => $"{BaseUrl}/identity/profile";
        public static string UpdateUserProfileUrl => $"{BaseUrl}/identity/profile";
        public static string ResetPasswordUrl => $"{BaseUrl}/identity/password/reset";
        public static string WalletUrl => $"{BaseUrl}/wallets";
        public static string WalletTransactionUrl => $"{BaseUrl}/wallet/transactions";
        public static string GetMembershipDownlineUrl => $"{BaseUrl}/membership/downlines/{memberID}";
        public static string GetMembershipDetailUrl => $"{BaseUrl}/membership/profile";
        public static string GetProductsUrl => $"{BaseUrl}/shopping/products";
        


        public static ProfileData UserProfileData { get; set; }
        public static GetMyWalletResponseModel UserWalletData { get; set; }
        public static string memberID { get; set; }


        public static bool Contains(this string target, string value, StringComparison comparison)
        {
            return target.IndexOf(value, comparison) >= 0;
        }
    }

}
