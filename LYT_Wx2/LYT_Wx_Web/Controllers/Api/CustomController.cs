using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    /// <summary>
    /// 客服消息
    /// </summary>
    public class CustomController: ApiController
    {
        [HttpPost]
        public WxResult send([FromUri]string token, [FromBody]ServiceMsg postData)
        {

            var appId = AccountHelper.GetAppId;
            var res = new WxResult();
            if (token == appId)
            {
                res.code = 1;
                try
                {
                    var appSecret = AccountHelper.GetAppSecret;
                    string accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                    foreach (var item in postData.List)
                    {
                        try
                        {
                            CustomApi.SendText(accessToken, item.openid, item.content);
                        }
                        catch (Exception e)
                        {
                            LogHelper.ExLog(e, " Custom/send",item.content+","+item.openid);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, " Custom/send");
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