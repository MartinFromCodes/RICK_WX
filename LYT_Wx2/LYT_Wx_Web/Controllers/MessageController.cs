using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    public class MessageController : BaseController
    {
        // GET: Message
        public ActionResult Index()
        {          
            return View();
        }
        public ActionResult test()
        {
            string urlFormat = "http://localhost:37363/api/Template/send?token=23";
           TemplateData m = new TemplateData();
            List<TemplateEntry> entrys = new List<TemplateEntry>();
            string[] str = new string[2] { "12","34"};
            TemplateEntry t = new TemplateEntry();
            t.data = str;
            t.openid = "333";
            t.order_no = "343";
            entrys.Add(t);
            m.type = 0;
            m.Entries = entrys.ToArray();
            var formDataBytes = m == null ? new byte[0] : Encoding.UTF8.GetBytes(LYT.Wx.MP.Helper.JsonHelper.Serialize(m));
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            Console.WriteLine("---1-----");
            var s=LYT.Wx.MP.HttpUtility.RequestUtility.HttpPostAsync(urlFormat,m);
            Console.WriteLine("---3-----");

            Console.WriteLine("---4-----");
            TemplateData mm = new TemplateData();
            mm.type = 5;
            var ss = LYT.Wx.MP.HttpUtility.RequestUtility.HttpPost(urlFormat, mm);
            Console.WriteLine("---5-----");
            return Content("");
        }
    }
}