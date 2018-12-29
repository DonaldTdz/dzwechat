using Senparc.CO2NET.Extensions;
using Senparc.NeuChar.Agents;
using Senparc.NeuChar.Entities;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.WechatMessages
{
    /// <summary>
    /// 事件请求处理
    /// </summary>
    public partial class CustomMessageHandler
    {
        public event EventHandler<RequestMessageEvent_Subscribe> OnSubscribe;    //关注事件
        public event EventHandler<RequestMessageEvent_Unsubscribe> OnUnsubscribe;//取消关注

        public override IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            // 预处理文字或事件类型请求。
            // 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
            // 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
            // 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
            // 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
            // 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey
            return null;//返回null，则继续执行OnTextRequest或OnEventRequest
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            //菜单点击，需要跟创建菜单时的Key匹配
            switch (requestMessage.EventKey)
            {
                case "dev":
                    {
                        //这个过程实际已经在OnTextOrEventRequest中完成，这里不会执行到。
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "功能在开发中，敬请期待...";
                        reponseMessage = strongResponseMessage;
                    }
                    break; 
                default: //默认key没有处理
                    {
                        //不回复任何信息
                        return new SuccessResponseMessage();
                    }
            }

            return reponseMessage;
        }

        /// <summary>
        /// 进入事件
        /// </summary>
        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            /*var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "您刚才发送了ENTER事件请求。";
            return responseMessage;*/
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 位置事件
        /// </summary>
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 通过二维码扫描关注扫描事件
        /// </summary>
        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 打开网页事件
        /// </summary>
        public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 群发完成事件
        /// </summary>
        public override IResponseMessageBase OnEvent_MassSendJobFinishRequest(RequestMessageEvent_MassSendJobFinish requestMessage)
        {
            //不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            //1、业务处理
            OnSubscribe?.Invoke(null, requestMessage);
            //2、图文消息
            if (!string.IsNullOrEmpty(Messages.SubscribeMsg))
            {
                var textResponseMessage = base.CreateResponseMessage<ResponseMessageText>();
                textResponseMessage.Content = Messages.SubscribeMsg;
                return textResponseMessage;
            } else if (Messages.SubscribeArticle != null)
            {
                var newsResponseMessage = base.CreateResponseMessage<ResponseMessageNews>();
                newsResponseMessage.ArticleCount = 1;
                newsResponseMessage.Articles.Add(Messages.SubscribeArticle);
                return newsResponseMessage;
            }
            //3、没有配置 不回复任何信息
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            OnUnsubscribe?.Invoke(null, requestMessage);
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之扫码推事件(scancode_push)
        /// </summary>
        public override IResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        {
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
        /// </summary>
        public override IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之弹出拍照或者相册发图（pic_photo_or_album）
        /// </summary>
        public override IResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之弹出系统拍照发图(pic_sysphoto)
        /// 实际测试时发现微信并没有推送RequestMessageEvent_Pic_Sysphoto消息，只能接收到用户在微信中发送的图片消息。
        /// </summary>
        public override IResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之弹出微信相册发图器(pic_weixin)
        /// </summary>
        public override IResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之弹出地理位置选择器（location_select）
        /// </summary>
        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之发送模板消息返回结果
        /// </summary>
        public override IResponseMessageBase OnEvent_TemplateSendJobFinishRequest(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            return null;
        }

        #region 微信认证事件推送

        public override IResponseMessageBase OnEvent_QualificationVerifySuccessRequest(RequestMessageEvent_QualificationVerifySuccess requestMessage)
        {
            return new SuccessResponseMessage();//返回"success"字符串
        }

        #endregion

        #region 异步方法

        public override Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            return Task.Factory.StartNew(() =>
            {
                var syncResponseMessage = OnEvent_ClickRequest(requestMessage);//这里为了保持Demo的连贯性，结果先从同步方法获取，实际使用过程中可以全部直接定义异步方法
                return syncResponseMessage;
            });
        }

        #endregion

    }
}
