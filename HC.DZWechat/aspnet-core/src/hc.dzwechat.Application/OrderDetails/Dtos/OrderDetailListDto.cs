

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.OrderDetails;
using static HC.DZWechat.DZEnums.DZEnums;
using Abp.AutoMapper;

namespace HC.DZWechat.OrderDetails.Dtos
{
    [AutoMapFrom(typeof(OrderDetail))]

    public class OrderDetailListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// OrderId
		/// </summary>
		[Required(ErrorMessage="OrderId不能为空")]
		public Guid OrderId { get; set; }



		/// <summary>
		/// GoodsId
		/// </summary>
		[Required(ErrorMessage="GoodsId不能为空")]
		public Guid GoodsId { get; set; }



		/// <summary>
		/// Specification
		/// </summary>
		[Required(ErrorMessage="Specification不能为空")]
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