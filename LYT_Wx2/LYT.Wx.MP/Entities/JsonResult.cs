using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYT.Wx.MP.Entities
{
   public  class JsonResult
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }

       public string result{get;set;}

       public int error_code{get;set;}

       public bool success{get;set;}

       public string error_msg{get;set;}

       

    }
}
