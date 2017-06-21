using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    public class WxMenuController : ApiController
    {
        [HttpPost]
        public WxResult create([FromUri]string token, [FromBody]MenuFullButtonGroup postData)
        {

            var appId = AccountHelper.GetAppId;
            var res = new WxResult();
            if (token == appId)
            {
                res.code = 1;
                LogHelper.Log("WxMenu/create", LYT.Wx.MP.Helper.JsonHelper.Serialize(postData));
               //string ss= "{ \"appid\":\"wx03c132a698ed2953\",\"appsecret\":\"6819cc0b320c341493f7e5f4be4acfb8\",\"button\":[{\"name\":\"微网站\",\"sub_button\":[{\"name\":\"企业简介\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://mp.weixin.qq.com/s/Wxc7Nhm7dvs7qtkOPeeKrQ\",\"tag\":1},{\"name\":\"荣誉介绍\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://mp.weixin.qq.com/mp/appmsg/show?__biz=MjM5NTAyNDc5OQ==\u0026appmsgid=10000014\u0026itemidx=1\u0026sign=d6df1652bafba676fb7b464c46c1e283\u0026scene=18#wechat_redirect\",\"tag\":1},{\"name\":\"公司网站\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://www.wejoin.com.cn/\",\"tag\":2},{\"name\":\"联系我们\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://mp.weixin.qq.com/s/zJy_ifko7dbg8UVzIUuPvA\",\"tag\":1}],\"type\":\"view\",\"url\":null,\"tag\":1},{\"name\":\"微服务\",\"sub_button\":[{\"name\":\"产品商城\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://wejoinwx.e2wan.com/Shop/Index\",\"tag\":0},{\"name\":\"配件商城\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://wejoinwx.e2wan.com/Shop/Part\",\"tag\":0}],\"type\":\"view\",\"url\":null,\"tag\":1},{\"name\":\"个人中心\",\"sub_button\":[{\"name\":\"我的订单\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://wejoinwx.e2wan.com/Shop/OrderList\",\"tag\":0},{\"name\":\"我的购物车\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://wejoinwx.e2wan.com/Shop/ShopCart\",\"tag\":0},{\"name\":\"我的消息\",\"sub_button\":null,\"type\":\"view\",\"url\":\"http://wejoinwx.e2wan.com/Message/Index?id=0\",\"tag\":1}],\"type\":\"view\",\"url\":null,\"tag\":1}]}";
              //  postData = JsonHelper.Deserialize<MenuFullButtonGroup>(ss);
               //return res;
                #region 菜单
                try
                {
                    appId = token;
                    var appsecret = string.IsNullOrEmpty(postData.appsecret) ? AccountHelper.GetAppSecret : postData.appsecret;
                    string accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appId, appsecret);
                    GetMenuResultFull json = new GetMenuResultFull();
                    MenuFull_ButtonGroup menu = new MenuFull_ButtonGroup();
                    List<MenuFull_RootButton> mb = new List<MenuFull_RootButton>();
                    foreach (var item in postData.button)
                    {
                        MenuFull_RootButton mr = new MenuFull_RootButton();
                        if (item.sub_button.Count() > 0)
                        {
                            List<MenuFull_RootButton> sub_mr = new List<MenuFull_RootButton>();
                            mr.name = item.name;
                            foreach (var it in item.sub_button)
                            {
                                MenuFull_RootButton s_mr = new MenuFull_RootButton();
                                s_mr.type = "view";
                                switch (it.tag)
                                {
                                    case 0:
                                        s_mr.url = OAuthApi.GetAuthorizeUrl(appId, it.url, "1", OAuthScope.snsapi_userinfo);
                                        break;
                                    case 1:
                                        s_mr.url = it.url;
                                        break;
                                    case 2:
                                        s_mr.url = it.url;
                                        break;
                                    default:
                                        s_mr.url = it.url;
                                        break;
                                }
                                s_mr.name = it.name;
                                sub_mr.Add(s_mr);
                            }
                            mr.sub_button = sub_mr;
                        }
                        else
                        {
                            mr.type = "view";
                            switch (item.tag)
                            {
                                case 0:
                                    mr.url = OAuthApi.GetAuthorizeUrl(appId, item.url, "1", OAuthScope.snsapi_userinfo);
                                    break;
                                case 1:
                                    mr.url = item.url;
                                    break;
                                case 2:
                                    mr.url = item.url;
                                    break;
                                default:
                                    mr.url = item.url;
                                    break;
                            }
                            mr.name = item.name;
                        }
                        mb.Add(mr);
                    }
                    menu.button = mb;
                    json.menu = menu;
                    var buttonGroup = CommonApi.GetMenuFromJsonResult(json, new ButtonGroup()).menu;
                    var result = CommonApi.CreateMenu(accessToken, buttonGroup);
                    LogHelper.Log("WxMenu/create", LYT.Wx.MP.Helper.JsonHelper.Serialize(result));
                    res.msg = result.errmsg;

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "WxMenu/create");
                    res.code = -1;
                    res.msg = "服务器异常，请重试";
                }
                #endregion
            }
            else
            {
                res.code = -1;
                res.msg = "token信息不合法";
            }
            return res;
        }        
    }
}