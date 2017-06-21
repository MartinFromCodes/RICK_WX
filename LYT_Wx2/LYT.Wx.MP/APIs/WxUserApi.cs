using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYT.Wx.MP.Entities;
using LYT.Wx.MP.Helper;
using LYT.Wx.MP.Account;
namespace LYT.Wx.MP.APIs
{
   public class WxUserApi
    {
        private static string ApiUrl = AccountHelper.GetApiUrl;
        /// <summary>
        /// add用户
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static JsonResult AddUser(string openid, Dictionary<string, string> parameters = null)
        {
            
            var url = ApiUrl + "/openapi/guanglianPdaApi";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("command", "WECHATUSERINFO");
            paras.Add("Params", JsonHelper.Serialize(parameters));
            paras.Add("Timestamp", DateTime.Now.ToString());
            paras.Add("digest", SecurityHelper.GetDigest(JsonHelper.Serialize(parameters)));
            
            try
            {
                var result = HttpUtility.Post.PostGetJson<JsonResult>(url, paras);              
                return result;
            }
            catch (Exception e)
            {
                LogHelper.ExLog(e, url);
                return new JsonResult();
            }

        }
        public static JsonResult GetSubscribeContent(string openid)
        {
            var url = ApiUrl + "/api/CtMall/GetFocus?UserToken=" + openid;
            try
            {
                var result = HttpUtility.Get.GetJson<JsonResult>(url);
                return result;
            }
            catch (Exception e)
            {
                LogHelper.ExLog(e, url);
                return new JsonResult();
            }

        }
    }
}
