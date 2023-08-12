using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.AuthFlow
{

    public class UpdateProfileRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
    }


}
