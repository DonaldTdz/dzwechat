
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.ShopCarts;

namespace HC.DZWechat.ShopCarts.Dtos
{
    public class GetShopCartsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
