using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.AuthFlow
{

    public class Verify2FARequestModel
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }


}
