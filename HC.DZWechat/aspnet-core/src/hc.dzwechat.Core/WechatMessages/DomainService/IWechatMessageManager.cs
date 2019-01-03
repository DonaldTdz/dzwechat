using Abp.Domain.Services;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.WechatMessages
{
    public interface IWechatMessageManager : IDomainService
    {
        Task<IMessageHandlerDocument> GetMessageHandlerAsync(Stream inputStream, PostModel postModel, int maxRecordCount = 0);

        Task<CustomMessages> GetCustomMessagesAsyncTest();
    }
}
