using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CBayMobileApp.Converters
{
    public static class StringHelper
    {
        public static string ToCustomString(TimeSpan span)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
        }
        public static bool StringToBool(this string input)
        {
            return !string.IsNullOrEmpty(input) && input.ToLower().Equals("true");
        }

        public static int ConvertStatusCode(int statusCode)
        {
            if (statusCode >= 200 && statusCode <= 207)
            {
                return 200;
            }
            if (statusCode >= 300 && statusCode <= 307)
            {
                return 300;
            }
            else if (statusCode >= 400 && statusCode <= 409)
            {
                return 400;
            }
            else if (statusCode >= 500 && statusCode <= 509)
            {
                return 500;
            }
            else
            {
                return 0;
            }
        }
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }
        public static string DecodeBase64StringToNormal(string encodedString)
        {
            byte[] data = Convert.FromBase64String(encodedString);
            return Encoding.UTF8.GetString(data);
        }
        public static string FormatAmountWithNaira(double amount)
        {
            var amountInNaira = Math.Round(amount, 2).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-us")).Replace("$", "NGN ");
            return amountInNaira;
        }

        public static string GetInitials(string name)
        {
            // StringSplitOptions.RemoveEmptyEntries excludes empty spaces returned by the Split method

            string[] nameSplit = name.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            string initials = "";

            for (int i = 0; i < 2; i++)
            {
                initials += nameSplit[i].Substring(0, 1).ToUpper();
            }

            return initials;
        }
    }

}
