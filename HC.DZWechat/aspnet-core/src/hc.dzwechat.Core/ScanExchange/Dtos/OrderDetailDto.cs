using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.OrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.ScanExchange.Dtos
{
    [AutoMapFrom(typeof(OrderDetail))]

    public class OrderDetailDto : EntityDto<Guid>, IHasCreationTime
    {
        /// <summary>
		/// OrderId
		/// </summary>
        public Guid OrderId { get; set; }



        /// <summary>
        /// GoodsId
        /// </summary>
        public Guid GoodsId { get; set; }



        /// <summary>
        /// Specification
        /// </summary>
        public string Specification { get; set; }



        /// <summary>
        /// Integral
        /// </summary>
        public decimal? Integral { get; set; }



        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; }



        /// <summary>
        /// Num
        /// </summary>
        public decimal? Num { get; set; }



        /// <summary>
        /// Status
        /// </summary>
        public ExchangeStatus? Status { get; set; }
        public string StatusName
        {
            get
            {
                return Status.ToString();
            }
        }


        /// <summary>
        /// ExchangeTime
        /// </summary>
        public DateTime? ExchangeTime { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }



        /// <summary>
        /// UserChooseType
        /// </summary>
        public string UserChooseType { get; set; }
    }
}
