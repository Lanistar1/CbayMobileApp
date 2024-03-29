﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CBayMobileApp.Models.Membership
{
//    public class Downline
//    {
//        public string memberName { get; set; }
//        public string memberPhone { get; set; }
//        public string memberID { get; set; }
//        public string userID { get; set; }
//    }

    //public class MembershipDetailModel
    //{
    //    public string memberID { get; set; }
    //    public string currentCadre { get; set; }
    //    public string joinedDate { get; set; }
    //    public string refCode { get; set; }
    //    public List<Downline> downlines { get; set; }
    //}


    public class MembershipData
    {
        public string memberID { get; set; }
        public string refMemberID { get; set; }
        public string currentCadre { get; set; }
        public string refCode { get; set; }
        public string userID { get; set; }
        public List<object> downlines { get; set; }
        public string memberName { get; set; }
        public string memberPhone { get; set; }
    }

    public class MembershipDetailModel
    {
        public MembershipData data { get; set; }
    }


}
