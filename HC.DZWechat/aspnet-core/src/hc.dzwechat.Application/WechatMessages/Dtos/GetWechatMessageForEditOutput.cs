

using System.Collections.Generic;
using Abp.Application.Services.Dto;
using HC.DZWechat.WechatMessages;

namespace HC.DZWechat.WechatMessages.Dtos
{
    public class GetWechatMessageForEditOutput
    {

        public WechatMessageEditDto WechatMessage { get; set; }

    }
}