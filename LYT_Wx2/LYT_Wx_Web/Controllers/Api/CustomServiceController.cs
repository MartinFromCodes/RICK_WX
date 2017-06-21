using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    /// <summary>
    /// 多客服接口管理
    /// </summary>
    public class CustomServiceController : ApiController
    {
        /// <summary>
        /// 添加客服账号
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <returns>ok</returns>
        [HttpPost]
        public WxResult add([FromUri]string token, [FromBody] KfAccount postData)
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
                    string pwd = Senparc.Weixin.MP.Helpers.MD5UtilHelper.GetMD5(postData.password, null);
                    var result= CustomServiceApi.AddCustom(accessToken, postData.kf_account, postData.nickname, pwd);
                    if(result.errmsg == "OK")
                    {
                        return res;
                    }

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "CustomService/add");
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
        /// <summary>
        /// 设置客服信息
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <returns>ok</returns>
        public WxResult update([FromUri]string token, [FromBody] KfAccount postData)
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
                    string pwd = Senparc.Weixin.MP.Helpers.MD5UtilHelper.GetMD5(postData.password, null);
                    var result = CustomServiceApi.UpdateCustom(accessToken, postData.kf_account, postData.nickname, pwd);
                    if (result.errmsg == "OK")
                    {
                        return res;
                    }

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "CustomService/update");
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
        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <returns>ok</returns>
        public WxResult uploadheadimg([FromUri]string token, string kfAccount, string headimg)
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
                    var result = CustomServiceApi.UploadCustomHeadimg(accessToken, kfAccount, headimg);
                    if (result.errmsg == "OK")
                    {
                        return res;
                    }

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "CustomService/uploadheadimg");
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
        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        [HttpPost]
        public WxResult del([FromUri]string token, [FromUri]string kfAccount)
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
                    var result = CustomServiceApi.DeleteCustom(accessToken, kfAccount);
                    if (result.errmsg == "OK")
                    {
                        return res;
                    }

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "CustomService/del");
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
        /// <summary>
        /// 获取客服基本信息
        /// </summary>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns>ok</returns>
        public WxResult getlist([FromUri]string token)
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
                    var result = CustomServiceApi.GetCustomBasicInfo(accessToken);
                    if (result.errmsg == "OK")
                    {
                        res.data = JsonHelper.Serialize(result.kf_list);
                        return res;
                    }

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "CustomService/getlist");
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
