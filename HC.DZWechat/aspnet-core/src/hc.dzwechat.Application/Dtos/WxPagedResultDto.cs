using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.Dtos
{
    public class WxPagedResultDto<T> : PagedResultDto<T>
    {
        public WxPagedResultDto(): base()
        {
        }

        public WxPagedResultDto(int totalCount, IReadOnlyList<T> items) : base(totalCount, items)
        {

        }

        public int PageSize { get; set; }

        public int PageTotal
        {
            get
            {
                if (TotalCount == 0)
                {
                    return 0;
                }

                if (TotalCount % PageSize > 0)
                {
                    return (TotalCount / PageSize) + 1;
                }

                return (TotalCount / PageSize);
            }
        }
    }
}
