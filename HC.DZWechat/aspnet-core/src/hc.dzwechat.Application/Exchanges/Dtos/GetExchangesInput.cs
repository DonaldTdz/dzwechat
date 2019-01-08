
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Exchanges;

namespace HC.DZWechat.Exchanges.Dtos
{
    public class GetExchangesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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

    }
}
