

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.WechatMessages;

namespace HC.DZWechat.WechatMessages.Dtos
{
    public class CreateOrUpdateWechatMessageInput
    {
        [Required]
        public WechatMessageEditDto WechatMessage { get; set; }

    }
}