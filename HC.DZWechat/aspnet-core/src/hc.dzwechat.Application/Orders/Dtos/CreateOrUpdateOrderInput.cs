

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Orders;

namespace HC.DZWechat.Orders.Dtos
{
    public class CreateOrUpdateOrderInput
    {
        [Required]
        public OrderEditDto Order { get; set; }

    }
}