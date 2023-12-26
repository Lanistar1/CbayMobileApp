using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static string GetMembershipSecondDownlineUrl => $"{BaseUrl}/membership/downlines";
        public static string GetMembershipDetailUrl => $"{BaseUrl}/membership/profile";
        public static string GetProductsUrl => $"{BaseUrl}/shopping/products";
        public static string AddShippingAddressUrl => $"{BaseUrl}/shopping/address";
        public static string GetMyShippingAddressUrl => $"{BaseUrl}/shopping/addresses";
        public static string PlaceOrderUrl => $"{BaseUrl}/shopping/order";
        public static string MyOrderUrl => $"{BaseUrl}/shopping/orders";
        public static string ForgotPasswordUrl => $"{BaseUrl}/identity/password/initiatereset";
        public static string WithdrawalUrl => $"{BaseUrl}/wallet/external/transfer";

        public static string FundUrl => $"{BaseUrl}/wallet/fund";

        public static string GetBankUrl => $"{BaseUrl}/wallet/external/flutterwave/banks";
        public static string ValidateAccountUrl => $"{BaseUrl}/wallet/external/account/validate";
        public static string SearchUrl => $"{BaseUrl}/shopping/search";
        public static string GetProductByIDUrl => $"{BaseUrl}/shopping/product";


        public static ProfileData UserProfileData { get; set; }
        public static List<WalletData> UserWalletData { get; set; }
        public static string memberID { get; set; }
        public static string FullName { get; set; }
        public static string WalletId { get; set; }
        public static int Balance { get; set; }
        public static string ProductQuantity { get; set; }
        public static int PriceTag { get; set; }
        public static DateTime DateType { get; set; }
        public static string GenderType { get; set; }
        public static string bankName { get; set; }
        public static string bank { get; set; }
        public static string OpratorId { get; set; }
        public static string SelectedWallet { get; set; }

        public static ObservableCollection<CartItem> myCarts { get; set; }
        public class CartItem
        {
            public string productID { get; set; }

            public string productName { get; set; }
            public string defaultPictureLocation { get; set; }
            public Int32 quantity { get; set; }
            public Int32 productPrice { get; set; }
            public Int32 ProductQantity { get; set; }

        }


        public static bool Contains(this string target, string value, StringComparison comparison)
        {
            return target.IndexOf(value, comparison) >= 0;
        }
    }

}
