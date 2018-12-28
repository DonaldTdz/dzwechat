
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.WechatMessages;

namespace HC.DZWechat.WechatMessages.Dtos
{
    public class GetWechatMessagesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
