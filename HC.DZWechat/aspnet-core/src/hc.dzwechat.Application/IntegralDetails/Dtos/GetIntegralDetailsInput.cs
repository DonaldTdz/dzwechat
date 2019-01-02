
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.IntegralDetails;

namespace HC.DZWechat.IntegralDetails.Dtos
{
    public class GetIntegralDetailsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public  string OpenId { get; set; }
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
