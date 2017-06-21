using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    public class UserController : ApiController
    {        
        public UserResult get([FromUri]string token, string next_openid)
        {          
            var appId = AccountHelper.GetAppId;
            var res = new UserResult();
            if (token == appId)
            {
                res.code = 1;
                try
                {
                    var appSecret = AccountHelper.GetAppSecret;
                    string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                    next_openid = string.IsNullOrEmpty(next_openid) ? "" : next_openid;
                    var user = UserApi.Get(accessToken, next_openid);                                   
                    res.code = 1;
                    res.msg = "";
                    res.data = user.data;
                    res.count = user.count;
                    res.total = user.total;
                    res.next_openid = user.next_openid;
                    res.errcode = user.errcode;
                    res.errmsg = user.errmsg;

                }
                catch (Exception ex)
                {
                    res.code = -1;
                    res.msg = ex.Message;
                }

            }
            else
            {
                res.code = -1;
                res.msg = "token信息不合法";
            }
            
            return res;
        }
    }
    public class UserInfoController : ApiController
    {
        [HttpGet]
        public WxResult get([FromUri]string token, [FromUri]string openid)
        {
            var appId = AccountHelper.GetAppId;
            var res = new WxResult();
            if (token == appId)
            {
                var appSecret = AccountHelper.GetAppSecret;
                string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                UserInfoJson user = UserApi.Info(accessToken, openid);
                JavaScriptSerializer js = new JavaScriptSerializer();
                res.data = js.Serialize(user);
                res.code = 1;
            }
                return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        [HttpPost]
        public UserInfoResult batchget([FromUri]string token, [FromBody]OpenId openIds)
        {
            var appId = AccountHelper.GetAppId;
            var res = new UserInfoResult();
            if (token == appId)
            {
                res.code = 1;
                try
                {
                    var appSecret = AccountHelper.GetAppSecret;
                    string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                    List<BatchGetUserInfoData> data = new List<BatchGetUserInfoData>();
                    foreach (var item in openIds.openid.Take(100))
                    {
                        data.Add(new BatchGetUserInfoData()
                        {
                            openid = item,
                            lang = Language.zh_CN
                        });
                    }
                    var user = UserApi.BatchGetUserInfo(accessToken, data);
                    res.code = 1;
                    res.msg = "";
                    res.user_info_list = user.user_info_list;
                    res.errcode = user.errcode;
                    res.errmsg = user.errmsg;

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "Template/send");
                    res.code = -1;
                    res.msg = "服务器异常，请重试";
                }

            }
            else
            {
                res.code = -1;
                res.msg = "token信息不合法";
            }

            return res;
        }       
    }
}
