using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.AuthFlow
{

    public class ProfileData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNo { get; set; }
        public string emailAddress { get; set; }
        public string userID { get; set; }
        public object displayName { get; set; }
        public object displayPicture { get; set; }
        public object displayBanner { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string country { get; set; }
    }

    public class GetProfileResponseModel
    {
        public ProfileData data { get; set; }
    }
}
