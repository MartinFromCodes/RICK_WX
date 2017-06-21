#region UEM License
/* 
 Copyright © 2004-2015 UEM Corporation
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
namespace LYT.Wx.MP.Account
{
   public class AccountHelper
   {      
       public static string GetLYTUrl
       {
           get
            {
                return WebConfigurationManager.AppSettings["LYTUrl"];
            }
       }
       public static string GetApiUrl
       {
            get
            {
                return WebConfigurationManager.AppSettings["ApiUrl"];
            }
          
       }
       public static string GetAppId
       {
            get
            {
                return WebConfigurationManager.AppSettings["ServerappId"];
            }
           
       }
       public static string GetAppSecret
       {
           get
            {
                return WebConfigurationManager.AppSettings["ServerappSecret"];
            }
           
       }
        public static string GetEncodingAESKey{
            get
            {
                return WebConfigurationManager.AppSettings["EncodingAESKey"];
            }
           
        }
       public static string GetYSHToken
       {
            get
            {
                return WebConfigurationManager.AppSettings["LYTToken"];
            }
           
       }
       public static bool GetYSHBeta(){
           bool result = false;
           bool.TryParse(WebConfigurationManager.AppSettings["LYTBeta"], out result);
           return result;
       }
       public static bool GetYSHLog()
       {
           bool result = false;
           bool.TryParse(WebConfigurationManager.AppSettings["LYTLog"], out result);
           return result;
       }
    }
}
