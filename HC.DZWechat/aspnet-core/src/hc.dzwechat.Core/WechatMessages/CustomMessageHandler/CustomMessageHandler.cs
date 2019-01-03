using Senparc.CO2NET.Helpers;
using Senparc.NeuChar.Agents;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Entities.Request;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace HC.DZWechat.WechatMessages
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        /*
         * 重要提示：v1.5起，MessageHandler提供了一个DefaultResponseMessage的抽象方法，
         * DefaultResponseMessage必须在子类中重写，用于返回没有处理过的消息类型（也可以用于默认消息，如帮助信息等）；
         * 其中所有原OnXX的抽象方法已经都改为虚方法，可以不必每个都重写。若不重写，默认返回DefaultResponseMessage方法中的结果。
         */

        //string agentUrl = "http://localhost:12222/App/Weixin/4";
        //string agentToken = "27C455F496044A87";
        //string wiweihiKey = "CNadjJuWzyX5bz5Gn+/XoyqiqMa5DjXQ";


        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

        /// <summary>
        /// 图文消息
        /// </summary>
        public CustomMessages Messages { get; set; }

        public ILogger Logger { get; set; }

        /// <summary>
        /// 模板消息集合（Key：checkCode，Value：OpenId）
        /// </summary>
        public static Dictionary<string, string> TemplateMessageCollection = new Dictionary<string, string>();

        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0) : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalGlobalMessageContext.ExpireMinutes = 3。
            GlobalMessageContext.ExpireMinutes = 3;

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }

            Messages = new CustomMessages();
            Logger = NullLogger.Instance;

            //在指定条件下，不使用消息去重
            //base.OmitRepeatedMessageFunc = requestMessage =>
            //{
            //    var textRequestMessage = requestMessage as RequestMessageText;
            //    if (textRequestMessage != null && textRequestMessage.Content == "容错")
            //    {
            //        return false;
            //    }
            //    return true;
            //};
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var requestHandler = requestMessage.StartHandler();
            //Logger.Info("msgs:"+JsonConvert.SerializeObject(Messages));

            //1、文字消息回复
            var textResponseMessage = base.CreateResponseMessage<ResponseMessageText>();
            foreach (var keyWord in Messages.KeyWords)
            {
                if (keyWord.Key == "默认")
                {
                    requestHandler.Default(() =>
                    {
                        textResponseMessage.Content = keyWord.Value;
                        return textResponseMessage;
                    });
                }
                else
                {
                    //如果有逗号表示数组
                    if (keyWord.Key.Contains(','))
                    {
                        requestHandler.Keywords(keyWord.Key.Split(','), () =>
                        {
                            textResponseMessage.Content = keyWord.Value;
                            return textResponseMessage;
                        });
                    }
                    //表示关键字
                    else
                    {
                        requestHandler.Keyword(keyWord.Key, () =>
                        {
                            textResponseMessage.Content = keyWord.Value;
                            return textResponseMessage;
                        });
                    }
                }
            }

            //2、图文消息回复
            var newsResponseMessage = base.CreateResponseMessage<ResponseMessageNews>();
            foreach (var keyWordPic in Messages.KeyWordsPic)
            {
                if (keyWordPic.Key == "默认")//注意： 如果配置了文字默认回复，则图文不会被回复
                {
                    requestHandler.Default(() =>
                    {
                        newsResponseMessage.ArticleCount = 1;
                        newsResponseMessage.Articles.Add(keyWordPic.Value);
                        return newsResponseMessage;
                    });
                }
                else
                {
                    //如果有逗号表示数组
                    if (keyWordPic.Key.Contains(','))
                    {
                        requestHandler.Keywords(keyWordPic.Key.Split(','), () =>
                        {
                            newsResponseMessage.ArticleCount = 1;
                            newsResponseMessage.Articles.Add(keyWordPic.Value);
                            return newsResponseMessage;
                        });
                    }
                    //表示关键字
                    else
                    {
                        requestHandler.Keyword(keyWordPic.Key, () =>
                        {
                            newsResponseMessage.ArticleCount = 1;
                            newsResponseMessage.Articles.Add(keyWordPic.Value);
                            return newsResponseMessage;
                        });
                    }
                }
            }

            //3、没有配置则默认 不回复任何消息
            //return new SuccessResponseMessage();

            return requestHandler.GetResponseMessage() as IResponseMessageBase;
        }

        /// <summary>
        /// 处理位置请求
        /// </summary>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            /*var locationService = new LocationService();
            var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            return responseMessage;*/
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 处理小视频请求
        /// </summary>
        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            /*var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您刚才发送的是小视频";
            return responseMessage;*/
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 处理图片请求
        /// </summary>
        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 处理视频请求
        /// </summary>
        public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }


        /// <summary>
        /// 处理链接消息请求
        /// </summary>
        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 处理文件请求
        /// </summary>
        public override IResponseMessageBase OnFileRequest(RequestMessageFile requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 没有被处理到的消息请求
        /// </summary>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 处理SDK没有提供的消息类型, 如：微信提供了新的消息类型，但SDK没更新 处理
        /// </summary>
        public override IResponseMessageBase OnUnknownTypeRequest(RequestMessageUnknownType requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }
    }
}
