using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Membership
{
    public class GetMembershipDownlineData
    {
        public List<DownlineData> downlines { get; set; }
    }

    public class DownlineData
    {
        public string memberID { get; set; }
        public string refMemberID { get; set; }
        public string currentCadre { get; set; }
        public string refCode { get; set; }
        public string userID { get; set; }
        public List<string> downlines { get; set; }
        public string memberName { get; set; }
        public string memberPhone { get; set; }
        public bool isSelected { get; set; }
    }


    public class GetMembershipDownlineModel
    {
        public GetMembershipDownlineData data { get; set; }
    }
}
