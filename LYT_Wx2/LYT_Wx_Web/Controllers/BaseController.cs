using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using Senparc.Weixin.MP.Containers;
using System.Collections.Generic;
using LYT.Wx.MP.Entities;

namespace LYT_Wx_Web.Controllers
{
    public class BaseController : Controller
    {
        protected string appId = AccountHelper.GetAppId;
        protected string secret = AccountHelper.GetAppSecret;
        protected string hostUrl = AccountHelper.GetLYTUrl;
        private string CONST_UNAME = "LYT_McroID";
        protected string openId = string.Empty;
        
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            string code = requestContext.HttpContext.Request.QueryString["code"];
            ViewBag.M_OpenId = openId = GetMicroID(code);
            
            base.Initialize(requestContext);
        }
        protected virtual string GetMicroID(string code)
        {
            string MicroID = "";
            if (!string.IsNullOrEmpty(GetCookieValue(CONST_UNAME)))
            {
                return MicroID = GetCookieValue(CONST_UNAME);
            }
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    MicroID = OAuthApi.GetAccessToken(appId, secret, code).openid;
                }
                //else
                //{
                //    return string.Empty;
                //}
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(GetCookieValue(CONST_UNAME)))
                {
                    MicroID = GetCookieValue(CONST_UNAME);
                }
                LogHelper.ExLog(e, "GetMicroID");
            }
            SetCookie(CONST_UNAME, MicroID, DateTime.Now.AddDays(1));
            LogHelper.Log(MicroID);
#if DEBUG
            if (string.IsNullOrEmpty(MicroID))
            {
                MicroID = "oSkYqvwSOTG85qx4DtorIP9g-bOk";
            }
#endif
            return MicroID;
        }
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void RemoveCookie(string cookiename)
        {
            if (!string.IsNullOrEmpty(GetCookieValue(cookiename)))
            {
                System.Web.HttpContext.Current.Response.Cookies.Remove(cookiename);
            }
        }
        
        protected JsSdkUiPackage GetJsSdkPackage()
        {
            string signature = string.Empty;
            string nonceStr = JSSDKHelper.GetNoncestr();
            string timestamp = JSSDKHelper.GetTimestamp();
            try
            {
                string ticket = JsApiTicketContainer.TryGetJsApiTicket(appId, secret);
                signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, GetAbsoluteUri());
            }
            catch (Exception e)
            {
                LogHelper.ExLog(e, Request.Url.AbsoluteUri);
                return null;
            }
            return new JsSdkUiPackage(appId, timestamp, nonceStr, signature);
        }
        protected virtual string GetAbsoluteUri()
        {
            return @"http://" + Request.Url.DnsSafeHost + Request.Url.PathAndQuery;
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (!string.IsNullOrEmpty(openId))
        //    {
        //        var url = AccountHelper.GetApiUrl +"/openapi/guanglianPdaApi";

        //        Dictionary<string, string> paras = new Dictionary<string, string>();

        //        Dictionary<string,string> parameters= new Dictionary<string,string>(){
        //             {"openid",openId}
        //        };

        //        paras.Add("command", "WXAGENTUSERINFO");
        //        paras.Add("Params", JsonHelper.Serialize(parameters));
        //        paras.Add("Timestamp", DateTime.Now.ToString());
        //        paras.Add("digest", SecurityHelper.GetDigest2("654321", JsonHelper.Serialize(parameters)));

        //        if (filterContext.ActionDescriptor.ActionName == "Sign")
        //        {
        //            try
        //            {
          //              var result = LYT.Wx.MP.HttpUtility.Post.PostGetJson<WxJsonResult>(url, paras);

        //                if (!result.success)
        //                {
        //                    filterContext.Result = RedirectToRoute("Default", new { Controller = "Home", Action = "Index" });

        //                } 
        //            }
        //            catch (Exception e)
        //            {
        //                LogHelper.ExLog(e, url+" | "+paras.ToString());
                       
        //            }
        //        }
              
        //    }
        //    base.OnActionExecuting(filterContext);
        //}
    }
}