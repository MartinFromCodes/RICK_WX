using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace LYT.Wx.MP.HttpUtility
{
   public static class Get
    {
        public static T GetJson<T>(string url, Encoding encoding = null)
        {
            string returnText = HttpUtility.RequestUtility.HttpGet(url, encoding);
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            T result = js.Deserialize<T>(returnText);

            return result;
        }
    }
}
