using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HC.DZWechat.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;

namespace HC.DZWechat.Web.Host.Controllers
{
    public class WechatMessageController : DZWechatControllerBase
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token ?? CheckSignature.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。

        readonly Func<string> _getRandomFileName = () => DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_Async_" + Guid.NewGuid().ToString("n").Substring(0, 6);


        public WechatMessageController()
        {
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://sdk.weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
        {
            return Task.Factory.StartNew(() =>
            {
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    return echostr; //返回随机字符串则表示验证通过
                }
                else
                {
                    return "failed:" + signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                        "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                }
            }).ContinueWith<ActionResult>(task => Content(task.Result));
        }

        //public CustomMessageHandler MessageHandler = null;//开放出MessageHandler是为了做单元测试，实际使用过程中不需要

        /// <summary>
        /// 最简化的处理流程
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
               // return new WeixinResult("参数错误！");
            }
            /*
            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey; //根据自己后台的设置保持一致
            postModel.AppId = AppId; //根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, 10);

            messageHandler.DefaultMessageHandlerAsyncEvent = Senparc.NeuChar.MessageHandlers.DefaultMessageHandlerAsyncEvent.SelfSynicMethod;//没有重写的异步方法将默认尝试调用同步方法中的代码（为了偷懒）

            #region 设置消息去重

            // 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
             //收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage
            messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

            #endregion

            messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

            await messageHandler.ExecuteAsync(); //执行微信处理过程（关键）

            messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

            //MessageHandler = messageHandler;//开放出MessageHandler是为了做单元测试，实际使用过程中这一行不需要

            return new FixWeixinBugWeixinResult(messageHandler);*/
            return Content("ok");
        }

    }
}