
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.VipPurchases;
using System;

namespace HC.DZWechat.VipPurchases.Dtos
{
    public class GetVipPurchasesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

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

        public Guid VipUserId { get; set; }

    }
}
