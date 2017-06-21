using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace LYT.Wx.MP.HttpUtility
{
  public static  class Post
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            T result = js.Deserialize<T>(returnText);
            return result;
        }
        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null)
        {
            string returnText = HttpUtility.RequestUtility.HttpPost(url, cookieContainer, formData, encoding);
            var result = GetResult<T>(returnText);
            return result;

        }
        public static T PostGetJson<T>(string url, Dictionary<string, string> formData = null)
        {
            string returnText = HttpUtility.RequestUtility.HttpPost(url, formData);
            var result = GetResult<T>(returnText);
            return result;
        }
    }
}
