using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYT_Wx_Web.Models
{
   
    public class ServiceMsg
    {
        public ServiceMsgItem[] List { get; set; }
    }
    public class ServiceMsgItem
    {
        public string openid { get; set; }
        public string content { get; set; }
    }
}