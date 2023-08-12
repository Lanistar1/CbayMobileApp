using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.AuthFlow
{

    public class Verify2FAResponseModel
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }


}
