using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.Orders.Dtos
{
    public class SaveOrderInput
    {
        public string WxOpenId { get; set; }

        public Guid DeliveryId { get; set; }

        public string Remark { get; set; }
        public Guid OrderId { get; set; }
        public string FormId { get; set; }
    }
}
