
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.WechatSubscribes.Dtos
{
    public class GetWechatSubscribesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
