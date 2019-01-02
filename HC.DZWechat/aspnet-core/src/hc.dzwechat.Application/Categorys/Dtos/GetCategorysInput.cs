
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.Categorys;

namespace HC.DZWechat.Categorys.Dtos
{
    public class GetCategorysInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
