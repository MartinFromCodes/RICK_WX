using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LYT.Wx.MP.Account;
using LYT.Wx.MP.Helper;
using LYT_Wx_Web.Models;

namespace LYT_Wx_Web.Controllers
{
    /// <summary>
    /// 模板消息
    /// </summary>
    public class TemplateController : ApiController
    {
        [HttpPost]
        public WxResult send([FromUri]string token, [FromBody]TemplateData postData)
        {                      
            LogHelper.Log(JsonHelper.Serialize(postData));
            var appId = AccountHelper.GetAppId;
            var res = new WxResult();
            if (token == appId)
            {
                res.code = 1;
                try
                {
                    string templateid = string.Empty;
                    string url = string.Empty;
                    object data = null;
                    var appSecret = AccountHelper.GetAppSecret;
                    string accessToken =AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                    string currUrl = AccountHelper.GetLYTUrl;
                    #region
                    switch (postData.type)
                    {
                        case 0: //订单确认
                            templateid = "OPENTM401520260";                            
                            foreach (var item in postData.Entries)
                            {
                                try
                                {
                                    url = currUrl + "Shop/OrderDetail?id=" +item.order_no;
                                    data = new
                                    {
                                        first = new TemplateDataItem("您好，您的订单已确认"),
                                        keyword1 = new TemplateDataItem(item.data[0]),//订单号
                                        keyword2 = new TemplateDataItem(item.data[1]),//金额
                                        keyword3 = new TemplateDataItem(item.data[2]),//日期
                                        remark = new TemplateDataItem("请尽快完成支付")
                                    };
                                    TemplateApi.SendTemplateMessage(accessToken, item.openid, templateid, url, data);
                                }
                                catch (Exception e)
                                {

                                    LogHelper.ExLog(e, "Template/Send", item.ToString()+","+item.openid);
                                }
                               
                            }
                            break;
                        case 1://订单提交成功通知
                            templateid = "su6mndagBDxq1tt37VJbgwXW3LT6vcnw9IrAocg1B9g";
                            foreach (var item in postData.Entries)
                            {
                                try
                                {
                                    url = currUrl + "Shop/OrderDetail?id=" + item.order_no;
                                    data = new
                                    {
                                        first = new TemplateDataItem("您好，您已成功提交订单"),
                                        keyword1 = new TemplateDataItem(item.data[0]),//产品名称
                                        keyword2 = new TemplateDataItem(item.data[1]),//订单金额
                                        keyword3 = new TemplateDataItem(item.data[2]),//下单时间
                                        keyword4 = new TemplateDataItem(item.data[3]),//订单编号
                                        remark = new TemplateDataItem("感谢您的光临,请等待管理员确认后完成支付")
                                    };
                                    TemplateApi.SendTemplateMessage(accessToken, item.openid, templateid, url, data);
                                }
                                catch (Exception e)
                                {

                                    LogHelper.ExLog(e, "Template/Send", item.ToString() + "," + item.openid);
                                }
                                
                            }
                            break;
                        case 3://新订单通知
                            templateid = "WDzJJCS-O3RcCxlTuvFJ5hJQ-AjG8uby6gUN9S-QL28";
                            foreach (var item in postData.Entries)
                            {
                                try
                                {
                                    url = string.Empty; 
                                    data = new
                                    {
                                        first = new TemplateDataItem("有客户通过微信成功下单"),
                                        keyword1 = new TemplateDataItem(item.data[0]),//订单编号
                                        keyword2 = new TemplateDataItem(item.data[1]),//客户手机                                       
                                        remark = new TemplateDataItem("请及时处理订单信息")
                                    };
                                    TemplateApi.SendTemplateMessage(accessToken, item.openid, templateid, url, data);
                                }
                                catch (Exception e)
                                {

                                    LogHelper.ExLog(e, "Template/Send", item.ToString() + "," + item.openid);
                                }

                            }
                            break;
                        case 4://订单支付通知
                            templateid = "YkJqpBME9m0L5Jdi5vyhRybU9IX__FVz6ks3pBlhy-M";
                            foreach (var item in postData.Entries)
                            {
                                try
                                {
                                    url = string.Empty;
                                    data = new
                                    {
                                        first = new TemplateDataItem("您的订单已支付成功"),
                                        keyword1 = new TemplateDataItem(item.data[0]),//订单商品
                                        keyword2 = new TemplateDataItem(item.data[1]),//订单编号 
                                        keyword3 = new TemplateDataItem(item.data[2]),//支付金额
                                        keyword4 = new TemplateDataItem(item.data[3]),//支付时间                                
                                        remark = new TemplateDataItem("感谢您的光临")
                                    };
                                    TemplateApi.SendTemplateMessage(accessToken, item.openid, templateid, url, data);
                                }
                                catch (Exception e)
                                {

                                    LogHelper.ExLog(e, "Template/Send", item.ToString() + "," + item.openid);
                                }

                            }
                            break;
                        case 5://订单处理提醒
                            templateid = "6kvsUdCIYq3f8WVTHB8JE2IMs6hEnV80UzE7gpUHPzg";
                            foreach (var item in postData.Entries)
                            {
                                try
                                {
                                    url = string.Empty;
                                    data = new
                                    {
                                        first = new TemplateDataItem("您好，您的订单已发货"),
                                        keyword1 = new TemplateDataItem(item.data[0]),//订单编号
                                        keyword2 = new TemplateDataItem(item.data[1]),//物流公司
                                        keyword3 = new TemplateDataItem(item.data[2]),//物流单号                                                                   
                                        remark = new TemplateDataItem("感谢您的支持！请注意跟进物流信息，及时查收快件。")
                                    };
                                    TemplateApi.SendTemplateMessage(accessToken, item.openid, templateid, url, data);
                                }
                                catch (Exception e)
                                {

                                    LogHelper.ExLog(e, "Template/Send", item.ToString() + "," + item.openid);
                                }

                            }
                            break;
                        case 6://订单关闭提醒
                            templateid = "OPENTM201764653";
                            foreach (var item in postData.Entries)
                            {
                                try
                                {
                                    url = string.Empty;
                                    data = new
                                    {
                                        first = new TemplateDataItem("您好，您的订单已取消"),
                                        keyword1 = new TemplateDataItem(item.data[0]),//订单商品
                                        keyword2 = new TemplateDataItem(item.data[1]),//订单编号
                                        keyword3 = new TemplateDataItem(item.data[2]),//下单时间   
                                        keyword4 = new TemplateDataItem(item.data[3]),//订单金额
                                        keyword5 = new TemplateDataItem(item.data[4]),//关闭时间                                                                
                                        remark = new TemplateDataItem("感谢您关注，如有疑问请联系客服")
                                    };
                                    TemplateApi.SendTemplateMessage(accessToken, item.openid, templateid, url, data);
                                }
                                catch (Exception e)
                                {

                                    LogHelper.ExLog(e, "Template/Send", item.ToString() + "," + item.openid);
                                }

                            }
                            break;
                        default:
                            break;
                    }
                    #endregion                                   

                }
                catch (Exception e)
                {
                    LogHelper.ExLog(e, "Template/send");
                    res.code = -1;
                    res.msg = "服务器异常，请重试";
                }

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
