

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.WechatUsers;

namespace HC.DZWechat.WechatUsers.Dtos
{
    public class CreateOrUpdateWechatUserInput
    {
        [Required]
        public WechatUserEditDto WechatUser { get; set; }

    }
}