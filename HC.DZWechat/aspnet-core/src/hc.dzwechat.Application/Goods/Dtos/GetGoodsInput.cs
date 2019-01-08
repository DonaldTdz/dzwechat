
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Goods;

namespace HC.DZWechat.Goods.Dtos
{
    public class GetGoodsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string NodeKey { get; set; }
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
