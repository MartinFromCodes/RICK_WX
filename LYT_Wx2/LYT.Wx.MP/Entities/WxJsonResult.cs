using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYT.Wx.MP.Entities
{
    public class WxJsonResult
    {
        public object result { get; set; }

        public int error_code { get; set; }

        public bool success { get; set; }

        public string error_msg { get; set; }
    }

    //public class Result
    //{
    //    public string nickname { get; set; }

    //    public string usercode { get; set; }
    //}

    public class WxUnResult
    {
        public string result { get; set; }
        public int error_code { get; set; }
        public bool success { get; set; }
        public string error_msg { get; set; }

    }
    public class DigResult
    {
        public string result { get; set; }
    }
}
