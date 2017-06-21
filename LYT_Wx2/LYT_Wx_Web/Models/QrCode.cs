using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYT_Wx_Web.Models
{
    public class QrCode
    {
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天），此字段如果不填，则默认为永久。
        /// </summary>
        public int expire_seconds { get; set; }
        /// <summary>
        /// 二维码类型，0为临时,1为永久,2为永久的字符串参数值
        /// </summary>
        public int action_name { get; set; }
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public int scene_id { get; set; }
        /// <summary>
        /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段 
        /// </summary>
        public string scene_str { get; set; }
    }
    /// <summary>
    /// 返回数据
    /// </summary>
    //public class QrCodeResult : WxResult
    //{
    //    public string url { get; set; }
    //}
}