using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYT_Wx_Web.Models
{
    /// <summary>
    /// 组菜单
    /// </summary>
    public class MenuFullButtonGroup
    {
        /// <summary>
        /// 可选参数
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 可选参数
        /// </summary>
        public string appsecret { get; set; }
        public List<MenuFullRootGroup> button { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class MenuFullRootGroup
    {        
        public string name { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuFullRootGroup> sub_button { get; set; }
        /// <summary>
        /// 可选参数  默认值view
        /// </summary>
        public string type { get; set; }
        public string url { get; set; }
        /// <summary>
        /// /tag = 0代表需要OAuth认证,tag=1代表不需要OAuth认证，tag=2代表外部链接
        /// </summary>
        public int tag { get; set; }
    }
}