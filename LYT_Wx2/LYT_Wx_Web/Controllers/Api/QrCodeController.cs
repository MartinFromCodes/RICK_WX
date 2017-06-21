using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Web.Http;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    public class QrCodeController : ApiController
    {
        [HttpPost]
        public WxResult getqrcode([FromUri]string token, [FromBody]QrCode data)
        {

            var appId = AccountHelper.GetAppId;
            var res = new WxResult();
            if (token == appId)
            {
                res.code = 1;
                try
                {
                    var appSecret = AccountHelper.GetAppSecret;
                    string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                    var qrResult = QrCodeApi.Create(accessToken, data.expire_seconds, data.scene_id, (QrCode_ActionName)data.action_name, data.scene_str);
                    res.data = QrCodeApi.GetShowQrCodeUrl(qrResult.ticket);

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "qr/getqrcode");
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
