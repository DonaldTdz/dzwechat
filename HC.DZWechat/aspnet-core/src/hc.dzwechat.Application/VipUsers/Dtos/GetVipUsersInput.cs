
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.VipUsers;

namespace HC.DZWechat.VipUsers.Dtos
{
    public class GetVipUsersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
