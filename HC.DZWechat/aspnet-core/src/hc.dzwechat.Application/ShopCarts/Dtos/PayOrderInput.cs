using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.ShopCarts.Dtos
{
    public class PayOrderInput
    {
        public string WxOpenId { get; set; }

        public List<UserSelectedCart> Items { get; set; }
    }

    public class UserSelectedCart : EntityDto<Guid>
    {
        public int Num { get; set; }
    }
}
