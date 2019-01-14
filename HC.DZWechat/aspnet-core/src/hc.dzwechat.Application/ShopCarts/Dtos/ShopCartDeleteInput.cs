using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.ShopCarts.Dtos
{
    public class ShopCartDeleteInput
    {
        public string WxOpenId { get; set; }

        public Guid ShopCartId { get; set; }
    }
}
