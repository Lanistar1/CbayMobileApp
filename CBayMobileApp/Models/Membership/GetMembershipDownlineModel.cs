using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Membership
{

    public class DownlineData
    {
        public string memberName { get; set; }
        public string memberPhone { get; set; }
        public string memberID { get; set; }
        public string cadre { get; set; }
        public string dateJoined { get; set; }
        public string userID { get; set; }
    }

    public class GetMembershipDownlineModel
    {
        public List<DownlineData> downlines { get; set; }
    }

}
