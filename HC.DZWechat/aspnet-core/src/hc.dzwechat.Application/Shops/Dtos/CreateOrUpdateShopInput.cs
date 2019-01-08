

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Shops;

namespace HC.DZWechat.Shops.Dtos
{
    public class CreateOrUpdateShopInput
    {
        [Required]
        public ShopEditDto Shop { get; set; }

    }
}