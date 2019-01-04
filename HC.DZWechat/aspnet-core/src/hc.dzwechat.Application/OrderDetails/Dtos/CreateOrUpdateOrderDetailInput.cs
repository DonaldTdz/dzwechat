

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.OrderDetails;

namespace HC.DZWechat.OrderDetails.Dtos
{
    public class CreateOrUpdateOrderDetailInput
    {
        [Required]
        public OrderDetailEditDto OrderDetail { get; set; }

    }
}