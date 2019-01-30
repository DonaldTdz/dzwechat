using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HC.DZWechat.Configuration;
using HC.DZWechat.Controllers;
using HC.DZWechat.Dtos;
using HC.DZWechat.ScanExchange;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Helpers;

namespace HC.DZWechat.Web.Host.Controllers
{
    public class WechatController : DZWechatControllerBase
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token ?? CheckSignature.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string AppSecret = Config.SenparcWeixinSetting.WeixinAppSecret;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        //private string host = "http://localhost:21021";
        //private string host = "http://hcwx.sayequ.me";
        private string host = "https://wx.hechuangcd.com";
        private readonly IConfigurationRoot _appConfiguration;
        private IScanExchangeManager _scanExchangeManager;

        private string UserOpenId
        {
            get
            {
                if (HttpContext.Session.GetString("DZUserOpenId") == null)
                {
                    return string.Empty;
                }
                return HttpContext.Session.GetString("DZUserOpenId");
            }
            set
            {
                value = value ?? string.Empty;
                HttpContext.Session.SetString("DZUserOpenId", value);
            }
        }

        private string UserUnionid
        {
            get
            {
                if (HttpContext.Session.GetString("DZUserUnionid") == null)
                {
                    return string.Empty;
                }
                return HttpContext.Session.GetString("DZUserUnionid");
            }
            set
            {
                value = value ?? string.Empty;
                HttpContext.Session.SetString("DZUserUnionid", value);
            }
        }

        public WechatController(IHostingEnvironment env
                       ,IScanExchangeManager scanExchangeManager

            )
        {
            _appConfiguration = env.GetAppConfiguration();
            host = _appConfiguration["App:ServerRootAddress"];
            _scanExchangeManager = scanExchangeManager;
        }

        private async Task SetUserOpenId(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                //如果userId为null 则需要通过code重新获取
                if (string.IsNullOrEmpty(UserOpenId))
                {
                    try
                    {
                        var oauth = await OAuthApi.GetAccessTokenAsync(AppId, AppSecret, code, "authorization_code");
                        UserOpenId = oauth.openid;
                        UserUnionid = oauth.unionid;
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("GetAccessTokenAsync Exception:{0}", ex.Message);
                    }
                }
            }
        }

        public IActionResult GetCurrentUserOpenId()
        {
            APIResultDto result = new APIResultDto();
            //UserOpenId = "666666";
            //UserOpenId = "oL5qB1m9WA2oNqtszJOJWrcpErzk";
            if (string.IsNullOrEmpty(UserOpenId))
            {
                result.Code = 901;
                result.Msg = "用户没有登录";
            }
            else
            {
                result.Code = 0;
                result.Msg = "获取成功";
                result.Data = new { openId = UserOpenId, unionid = UserUnionid };
            }
            return Json(result);
        }

        /// <summary>
        /// 获取JS SDK配置
        /// </summary>
        public async Task<IActionResult> GetJsApiConfigAsync(string url)
        {
            var jsApiConfig = await JSSDKHelper.GetJsSdkUiPackageAsync(AppId, AppSecret, url);
            return Json(jsApiConfig);
        }

        [HttpGet]
        public IActionResult Authorization(AuthorizationPageEnum page, string param)
        {
            //UserOpenId = "9A7C8776-A623-473F-AF29-10D3E79A2FAE";
            var url = string.Empty;
            switch (page)
            {
                case AuthorizationPageEnum.NewsStudy: //烟雨课堂
                    {
                        //if (!string.IsNullOrEmpty(UserOpenId))//如果已获取当前openId 直接跳转到个人中心
                        //{
                        //    return Redirect(AuthorizationPageUrl.NewsStudyUrl);
                        //}
                        //url = host + "/Wechat/NewsStudyAsync";
                        return Redirect(AuthorizationPageUrl.NewsStudyUrl);
                    }
                case AuthorizationPageEnum.NewsLaunch: //新品快讯
                    {
                        return Redirect(AuthorizationPageUrl.NewsLaunchUrl);
                    }
                case AuthorizationPageEnum.NewsProduct: //产品大全
                    {
                        return Redirect(AuthorizationPageUrl.NewsProductUrl);
                    }
                case AuthorizationPageEnum.MarketingStrategy:
                    {
                        //if (!string.IsNullOrEmpty(UserOpenId))
                        //{
                        //    return Redirect(AuthorizationPageUrl.MarketingStrategyUrl);
                        //}
                        //url = host + "/Wechat/MarketingStrategyAsync";
                        return Redirect(AuthorizationPageUrl.MarketingStrategyUrl);
                    }
                case AuthorizationPageEnum.MarketingExchange: //扫码兑换
                    {
                        if (!string.IsNullOrEmpty(UserOpenId))
                        {
                            return Redirect(AuthorizationPageUrl.MarketingExchangeUrl);
                        }
                        url = host + "/Wechat/MarketingExchange";
                        //return Redirect(AuthorizationPageUrl.MarketingExchangeUrl);
                    }
                    break;
                case AuthorizationPageEnum.MarketingExchangeWithOrderId: //扫码兑换二维码
                    {
                        if (!string.IsNullOrEmpty(UserOpenId))
                        {
                            var result = _scanExchangeManager.GetIsAttentionByIdAsync(UserOpenId);
                            if(result == true)
                            {
                                return Redirect(string.Format(AuthorizationPageUrl.MarketingExchangeWithOrderIdUrl, param));
                            }
                            else
                            {
                                return RedirectToAction("ExchangeAuth");
                            }
                        }
                        else
                        {
                            url = host + "/Wechat/MarketingExchangeWithOrderId";
                            //param = param ?? "123";
                            //var pageUrl = OAuthApi.GetAuthorizeUrl(AppId, url, param, OAuthScope.snsapi_base, "code");
                            //return Redirect(pageUrl); 
                        }
                    }
                    break;
                default:
                    {
                        return Redirect("/dzwechat/index.html");
                    }
            }

            param = param ?? "123";
            var pageUrl = OAuthApi.GetAuthorizeUrl(AppId, url, param, OAuthScope.snsapi_base, "code");
            return Redirect(pageUrl);
        }

        /// <summary>
        /// 扫码兑换页面微信验证
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizeUrl(string orderId)
        {
            Logger.Info(orderId);
            string redictUrl = host + "/Wechat/MarketingExchangeWithOrderIdUrl";

            var url =
                string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect",
                                AppId, redictUrl, "code", 0, orderId);
            return url;
        }

        /// <summary>
        /// 微信二维码
        /// </summary>
        public IActionResult ExchangeAuth()
        {
            return View("wechatQrCode");
        }


    /// <summary>
    /// 烟雨课堂
    /// </summary>
    public async Task<IActionResult> NewsStudy(string code, string state)
        {
            await SetUserOpenId(code);

            return Redirect(AuthorizationPageUrl.NewsStudyUrl);
        }

        /// <summary>
        /// 营销策略
        /// </summary>
        public async Task<IActionResult> MarketingStrategy(string code, string state)
        {
            await SetUserOpenId(code);

            return Redirect(AuthorizationPageUrl.MarketingStrategyUrl);
        }

        /// <summary>
        /// 扫码兑换
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<IActionResult> MarketingExchange(string code, string state)
        {
            await SetUserOpenId(code);

            return Redirect(AuthorizationPageUrl.MarketingExchangeUrl);
        }

        /// <summary>
        /// 扫码兑换订单
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<IActionResult> MarketingExchangeWithOrderId(string code, string state)
        {
            await SetUserOpenId(code);
            Logger.Info(code);
            Logger.Info(UserOpenId);
            return Redirect(AuthorizationPageUrl.MarketingExchangeWithOrderIdUrl);
        }
    }


    public enum AuthorizationPageEnum
    {
        NewsStudy = 1,
        NewsLaunch = 2,
        NewsProduct = 3,

        MarketingStrategy = 101,
        MarketingExchange = 102,
        MarketingExchangeWithOrderId = 103,
    }

    public class AuthorizationPageUrl
    {
        public static string NewsStudyUrl = "/dzwechat/index.html#/news-study/study";
        public static string NewsLaunchUrl = "/dzwechat/index.html#/news-launch/launch";
        public static string NewsProductUrl = "/dzwechat/index.html#/news-product/product";

        public static string MarketingStrategyUrl = "/dzwechat/index.html#/marketing-strategy/strategy";
        public static string MarketingExchangeUrl = "/dzwechat/index.html#/marketing-exchange/exchange";
        public static string MarketingExchangeWithOrderIdUrl = "/dzwechat/index.html#/marketing-exchange/exchange;orderId={0}";
    }
}