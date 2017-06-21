using Senparc.Weixin.MP.AdvancedAPIs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYT_Wx_Web.Models
{
    public class UserResult : OpenIdResultJson
    {
        public int code { get; set; }
        public string msg { get; set; }
    }
    public class UserInfoResult : BatchGetUserInfoJsonResult
    {
        public int code { get; set; }
        public string msg { get; set; }
    }
    public class OpenId
    {
        public List<string> openid { get; set; }
    }
}