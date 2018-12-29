

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Goods;

namespace HC.DZWechat.Goods.Dtos
{
    public class CreateOrUpdateGoodInput
    {
        [Required]
        public GoodEditDto Good { get; set; }

    }
}