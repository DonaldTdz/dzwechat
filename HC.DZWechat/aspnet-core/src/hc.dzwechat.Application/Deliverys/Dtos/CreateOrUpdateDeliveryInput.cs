

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Deliverys;

namespace HC.DZWechat.Deliverys.Dtos
{
    public class CreateOrUpdateDeliveryInput
    {
        [Required]
        public DeliveryEditDto Delivery { get; set; }

    }
}