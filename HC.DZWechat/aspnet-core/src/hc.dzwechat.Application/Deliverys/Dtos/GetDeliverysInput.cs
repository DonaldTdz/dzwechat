
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Deliverys;

namespace HC.DZWechat.Deliverys.Dtos
{
    public class GetDeliverysInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string UnionId { get; set; }
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
