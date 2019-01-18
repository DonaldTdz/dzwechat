
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.DZEnums.DZCommonEnums;
using System;

namespace HC.DZWechat.Orders.Dtos
{
    public class GetOrdersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid Id { get; set; }
        public OrderStatus? Status { get; set; }

        public bool IsUnMailing { get; set; }
        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }

    public class GetWxOrderInput : WxPagedInputDto
    {
        public string WxOpenId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus? status { get; set; }
    }
}
