using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYT_Wx_Web.Models
{
    public class TemplateData
    {
        /// <summary>
        /// 接收者
        /// </summary>
        public TemplateEntry[] Entries { get; set; }
        /// <summary>
        /// 模板类型
        /// </summary>
        public int type { get; set; }
        
    }
    public class TemplateEntry
    {
        /// <summary>
        /// 接收者
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string[] data { get; set; }
        public override string ToString()
        {
            return string.Join(",", data);
        }
    }
}