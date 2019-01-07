using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HC.DZWechat.Dtos
{
    public class WxPagedInputDto: IPagedResultRequest
    {
        public int Page { get; set; }

        public int Size
        {
            get
            {
                return MaxResultCount;
            }
            set
            {
                if (value != 0)
                {
                    MaxResultCount = value;
                }
            }
        }

        [Range(1, AppLtmConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        private int skipCount;

        [Range(0, int.MaxValue)]
        public int SkipCount
        {
            get
            {
                if (Page == 0)
                {
                    return skipCount;
                }
                skipCount = (Page - 1) * MaxResultCount;
                return skipCount;
            }
            set
            {
                skipCount = value;
            }
        }

        public WxPagedInputDto()
        {
            MaxResultCount = AppLtmConsts.DefaultPageSize;
        }
    }
}
