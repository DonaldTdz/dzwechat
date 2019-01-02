
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.WechatUsers;

namespace HC.DZWechat.WechatUsers.Dtos
{
    public class GetWechatUsersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
