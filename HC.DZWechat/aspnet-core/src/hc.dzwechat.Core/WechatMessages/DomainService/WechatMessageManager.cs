using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Abp.Domain.Repositories;
using HC.DZWechat.WechatSubscribes;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Senparc.NeuChar.Entities;

namespace HC.DZWechat.WechatMessages
{
    public class WechatMessageManager : DZWechatDomainServiceBase, IWechatMessageManager
    {
        private readonly IRepository<WechatMessage, Guid> _wechatmessageRepository;
        private readonly IRepository<WechatSubscribe, Guid> _wechatsubscribeRepository;

        public WechatMessageManager(IRepository<WechatMessage, Guid> wechatmessageRepository,
            IRepository<WechatSubscribe, Guid> wechatsubscribeRepository)
        {
            _wechatmessageRepository = wechatmessageRepository;
            _wechatsubscribeRepository = wechatsubscribeRepository;
        }

        public async Task<IMessageHandlerDocument> GetMessageHandlerAsync(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
        {
            CustomMessageHandler messageHandler = new CustomMessageHandler(inputStream, postModel, maxRecordCount);
            messageHandler.Messages = await GetCustomMessagesAsync();
            messageHandler.OnSubscribe += MessageHandler_OnSubscribe;
            messageHandler.OnUnsubscribe += MessageHandler_OnUnsubscribe;
            messageHandler.DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod; //没有重写的异步方法将默认尝试调用同步方法中的代码

            #region 设置消息去重
            //默认已经开启
            //messageHandler.OmitRepeatedMessage = true;
            #endregion
            messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）
            await messageHandler.ExecuteAsync(); //执行微信处理过程（关键）
            messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

            return messageHandler;
        }
        /// <summary>
        /// 获取图文消息
        /// </summary>
        private async Task<CustomMessages> GetCustomMessagesAsync()
        {
            var messages = new CustomMessages();
            //关注图文消息
            var subscribe = await _wechatsubscribeRepository.GetAll().FirstOrDefaultAsync();
            if (subscribe != null)
            {
                switch (subscribe.MsgType)
                {
                    case MsgTypeEnum.文字消息:
                        {
                            messages.SubscribeMsg = subscribe.Content;
                        }
                        break;
                    case MsgTypeEnum.图文消息:
                        {
                            messages.SubscribeArticle = new Article()
                            {
                                Title = subscribe.Title,
                                Description = subscribe.Desc,
                                PicUrl = subscribe.PicLink,
                                Url = subscribe.Url
                            };
                        }
                        break;
                }
            }
            //回复图文消息
            var msgs = await _wechatmessageRepository.GetAll().ToListAsync();
            foreach (var msg in msgs)
            {
                switch (msg.MsgType)
                {
                    case MsgTypeEnum.文字消息:
                        {
                            //关键字
                            if (msg.TriggerType == TriggerTypeEnum.关键字)
                            {
                                messages.KeyWords[msg.KeyWord] = msg.Content;
                            }
                            else//事件
                            {
                                messages.EventKeies[msg.KeyWord] = msg.Content;
                            }
                        }
                        break;
                    case MsgTypeEnum.图文消息:
                        {
                            //关键字
                            if (msg.TriggerType == TriggerTypeEnum.关键字)
                            {
                                messages.KeyWordsPic[msg.KeyWord] = new Article()
                                {
                                    Title = msg.Title,
                                    Description = msg.Desc,
                                    PicUrl = msg.PicLink,
                                    Url = msg.Url
                                };
                            }
                            else//事件
                            { 
                                messages.EventKeiesPic[msg.KeyWord] = new Article()
                                {
                                    Title = msg.Title,
                                    Description = msg.Desc,
                                    PicUrl = msg.PicLink,
                                    Url = msg.Url
                                };
                            }
                        }
                        break;
                }
            }
            return messages;
        }
        /// <summary>
        /// 关注公众号
        /// </summary>
        private void MessageHandler_OnSubscribe(object sender, RequestMessageEvent_Subscribe e)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 取消关注
        /// </summary>
        private void MessageHandler_OnUnsubscribe(object sender, RequestMessageEvent_Unsubscribe e)
        {
            //throw new NotImplementedException();
        }      
    }
}
