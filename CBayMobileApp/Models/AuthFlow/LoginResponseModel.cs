﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.AuthFlow
{

    public class LoginResponseModel
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }


}
