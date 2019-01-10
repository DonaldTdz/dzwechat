

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.ShopCarts;

namespace HC.DZWechat.ShopCarts.Dtos
{
    public class CreateOrUpdateShopCartInput
    {
        [Required]
        public ShopCartEditDto ShopCart { get; set; }

    }
}