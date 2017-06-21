using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
namespace LYT.Wx.MP.Helper
{
    /// <summary>
    /// /序列化对象与后序列化
    /// </summary>
   public  class JsonHelper
    {
        /// <summary>
        /// 把json字符串转成对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="data">json字符串</param> 
        public static T Deserialize<T>(string data)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Deserialize<T>(data);
        }

        /// <summary>
        /// 把对象转成json字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string Serialize(object o)
        {
            StringBuilder sb = new StringBuilder();
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }
       /// <summary>
        /// 把对象转成byte数组
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="t"></param>
       /// <returns></returns>
        public static byte[] SerializeToByte<T>(T t)
        {
            MemoryStream mStream = new MemoryStream();
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(mStream, t);
            return mStream.GetBuffer();
        }
       /// <summary>
        /// 把字节数组转成对象

       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="b"></param>
       /// <returns></returns>
        public static  T Deserialize<T>(byte[] b)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            return (T)bFormatter.Deserialize(new MemoryStream(b));
        }


    }
}
