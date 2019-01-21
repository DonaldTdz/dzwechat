

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.VipPurchases;

namespace HC.DZWechat.VipPurchases.Dtos
{
    public class CreateOrUpdateVipPurchaseInput
    {
        [Required]
        public VipPurchaseEditDto VipPurchase { get; set; }

    }
}