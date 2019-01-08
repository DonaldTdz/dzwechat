

using System.Collections.Generic;
using Abp.Application.Services.Dto;
using HC.DZWechat.OrderDetails;

namespace HC.DZWechat.OrderDetails.Dtos
{
    public class GetOrderDetailForEditOutput
    {

        public OrderDetailEditDto OrderDetail { get; set; }

    }
}