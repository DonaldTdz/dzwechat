
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.OrderDetails;
using System;

namespace HC.DZWechat.OrderDetails.Dtos
{
    public class GetOrderDetailsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid OrderId { get; set; }
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
}
