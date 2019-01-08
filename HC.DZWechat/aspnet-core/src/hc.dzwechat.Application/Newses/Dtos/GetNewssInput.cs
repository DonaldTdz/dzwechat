
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Newses;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Newses.Dtos
{
    public class GetNewssInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 资讯类型
        /// </summary>
        public NewsType? NewsType { get; set; }

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
