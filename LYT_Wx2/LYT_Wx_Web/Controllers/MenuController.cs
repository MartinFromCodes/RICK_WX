using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using System.Xml;
using Senparc.Weixin.MP.AdvancedAPIs;
using LYT.Wx.MP.Account;

namespace LYT_Wx_Web.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.appId = AccountHelper.GetAppId;
            ViewBag.appSecret = AccountHelper.GetAppSecret;
            return View();
        }
        public ActionResult Msg()
        {
            return Content("即将开放，敬请期待！");
        }
        [HttpPost]
        public ActionResult Create(FormCollection Model)
        {
            string appId = Model["appId"];
            string appSecret = Model["appSecret"];
            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appSecret))
            {
                return View();
            }
            string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var json = GetMenuFromXml();
            var buttonGroup = CommonApi.GetMenuFromJsonResult(json, new ButtonGroup()).menu;
            var result = Senparc.Weixin.MP.CommonAPIs.CommonApi.CreateMenu(accessToken, buttonGroup);
            return Content("温馨提示：" + result.errmsg);
        }
        public GetMenuResultFull GetMenuFromXml()
        {
            string appId = AccountHelper.GetAppId;
            string hostUrl = AccountHelper.GetLYTUrl;
            string xmlFilePath = AppDomain.CurrentDomain.BaseDirectory + "Config/WeChatMenu.xml";
            if (string.IsNullOrEmpty(xmlFilePath))
            {
                return null;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlElement rootElem = xmlDoc.DocumentElement;   //获取根节点  
            XmlNodeList parentNodes = rootElem.ChildNodes;
            MenuFull_ButtonGroup menu = new MenuFull_ButtonGroup();
            List<MenuFull_RootButton> mb = new List<MenuFull_RootButton>();
            foreach (XmlNode parentNode in parentNodes)
            {
                XmlElement parentE = (XmlElement)parentNode;
                XmlNodeList subNodes = parentE.GetElementsByTagName("subbutton");
                MenuFull_RootButton mr = new MenuFull_RootButton();
                List<MenuFull_RootButton> sub_mr = new List<MenuFull_RootButton>();
                mr.name = parentE.SelectSingleNode("name").InnerText;
                var flag = parentE.GetAttribute("flag");
                if (flag == "true")
                {
                    var type = parentE.SelectSingleNode("type");
                    var name = parentE.SelectSingleNode("name");
                    mr.type = type.InnerText;
                    mr.name = name.InnerText;
                    var url = parentE.SelectSingleNode("url");
                    if (url != null)
                    {
                        XmlElement urlE = (XmlElement)url;
                        switch (urlE.GetAttribute("tag"))
                        {
                            case "1":
                                mr.url = OAuthApi.GetAuthorizeUrl(appId, hostUrl + url.InnerText, "1", OAuthScope.snsapi_userinfo);
                                break;
                            case "2":
                                mr.url = hostUrl + url.InnerText;
                                break;
                            case "3":
                                mr.url = url.InnerText;
                                break;
                            default:
                                if (url.InnerText.StartsWith("http://") || url.InnerText.StartsWith("https://"))
                                {
                                    mr.url = url.InnerText;
                                }
                                else
                                {
                                    mr.url = OAuthApi.GetAuthorizeUrl(appId, hostUrl + url.InnerText, "1", OAuthScope.snsapi_userinfo);
                                }
                                break;
                        }
                    }
                    else
                    {
                        var key = parentE.SelectSingleNode("key");
                        mr.key = key.InnerText;
                    }
                }
                else
                {//子菜单
                    foreach (var node in subNodes)
                    {
                        MenuFull_RootButton s_mr = new MenuFull_RootButton();
                        XmlElement subE = (XmlElement)node;
                        var type = subE.SelectSingleNode("type");
                        var name = subE.SelectSingleNode("name");
                        s_mr.type = type.InnerText;
                        s_mr.name = name.InnerText;
                        var url = subE.SelectSingleNode("url");

                        if (url != null)
                        {
                            XmlElement urlE = (XmlElement)url;
                            switch (urlE.GetAttribute("tag"))
                            {
                                case "1":
                                    s_mr.url = OAuthApi.GetAuthorizeUrl(appId, hostUrl + url.InnerText, "1", OAuthScope.snsapi_userinfo);
                                    break;
                                case "2":
                                    s_mr.url = hostUrl + url.InnerText;
                                    break;
                                case "3":
                                    s_mr.url = url.InnerText;
                                    break;
                                default:
                                    if (url.InnerText.StartsWith("http://") || url.InnerText.StartsWith("https://"))
                                    {
                                        s_mr.url = url.InnerText;
                                    }
                                    else
                                    {
                                        s_mr.url = OAuthApi.GetAuthorizeUrl(appId, hostUrl + url.InnerText, "1", OAuthScope.snsapi_userinfo);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            var key = subE.SelectSingleNode("key");
                            s_mr.key = key.InnerText;
                        }
                        sub_mr.Add(s_mr);
                        mr.sub_button = sub_mr;
                    }
                }

                mb.Add(mr);
            }
            menu.button = mb;
            GetMenuResultFull gf = new GetMenuResultFull();
            gf.menu = menu;           
            return gf;
        }
       

    }
    public class sss{
    public string Na { get; set; }
    }
}