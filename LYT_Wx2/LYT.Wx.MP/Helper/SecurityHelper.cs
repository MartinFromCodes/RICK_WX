using LYT.Wx.MP.Account;
using LYT.Wx.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LYT.Wx.MP.Helper
{
    public class SecurityHelper
    {
        /// <summary>
        /// MD5加密方法
        /// </summary>
        /// <param name="plainMessage"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string plainMessage)
        {
            byte[] buffer = MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainMessage));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < builder.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// MD5加密返回字节数组
        /// </summary>
        /// <param name="plainMessage"></param>
        /// <returns></returns>
        public static byte[] GetMD5ToByte(string plainMessage)
        {
            byte[] buffer = MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainMessage));

            return buffer;
        }

        public static string GetBASE64(byte[] bytes)
        {
            string result = "";
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                Console.WriteLine("转换出错");
            }
            return result;
        }

        /// <summary>
        /// 信息加密
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public static string GetDigest(string par)
        { 
            try
            {
                string key = "654321";
                byte[] bytes = GetMD5ToByte(par + key);
                return GetBASE64(bytes);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public static string GetDigest2(string key,string para)
        {

            var url = AccountHelper.GetApiUrl + "/openapi/createDigest";

           

            Dictionary<string, string> parameters = new Dictionary<string, string>(){
                     {"appKey",key},
                     {"Params",para}
                };

            string returnText = HttpUtility.RequestUtility.HttpPost(url, parameters);

            return returnText;

        }

    }
}
