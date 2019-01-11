using Abp.Application.Services.Dto;
using HC.DZWechat.DZEnums.DZCommonEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.ScanExchange.Dtos
{
    public class OrderDto : EntityDto<Guid>
    {
        public string Number { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public OrderStatus? Status { get; set; }

        public string StatusName
        {
            get
            {
                return Status.ToString();
            }
        }
    }
}
