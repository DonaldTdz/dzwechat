

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.WechatSubscribes.Dtos
{
    public class CreateOrUpdateWechatSubscribeInput
    {
        [Required]
        public WechatSubscribeEditDto WechatSubscribe { get; set; }

    }
}