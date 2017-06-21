using LYT.Wx.MP.Account;
using LYT.Wx.MP.Entities;
using LYT.Wx.MP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYT_Wx_Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Title = "UEM合作商平台";
            return View();
        }

        public ActionResult Sign()
        {
            ViewBag.JSSDKPackage = GetJsSdkPackage();
            ViewBag.Title = "签收单";
            return View();
        }

        public ActionResult SignTest()
        {
            ViewBag.JSSDKPackage = GetJsSdkPackage();
            ViewBag.Title = "签收单";
            return View();
        }


        public ActionResult UnBing()
        {
            ViewBag.Title = "更换绑定";
             
            return View();
        }

        public ActionResult TestUpload()
        {
            return View();
        }

        /// <summary>
        /// 派送异常查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SendExecption()
        {
            ViewBag.JSSDKPackage = GetJsSdkPackage();
            return View();
        }

    }
}
