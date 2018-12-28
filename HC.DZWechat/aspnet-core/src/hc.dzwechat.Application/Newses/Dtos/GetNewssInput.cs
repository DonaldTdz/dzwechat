
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Newses;

namespace HC.DZWechat.Newses.Dtos
{
    public class GetNewssInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
