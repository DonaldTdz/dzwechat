
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Deliverys;
using System;

namespace HC.DZWechat.Deliverys.Dtos
{
    public class GetDeliverysInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid UserId { get; set; }
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
