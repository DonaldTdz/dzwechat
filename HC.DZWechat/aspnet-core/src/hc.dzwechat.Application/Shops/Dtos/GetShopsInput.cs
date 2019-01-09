
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Shops;

namespace HC.DZWechat.Shops.Dtos
{
    public class GetShopsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
