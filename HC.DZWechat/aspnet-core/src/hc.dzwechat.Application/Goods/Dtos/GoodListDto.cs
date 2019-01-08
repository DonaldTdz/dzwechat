

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Goods;
using Abp.AutoMapper;

namespace HC.DZWechat.Goods.Dtos
{
    [AutoMapFrom(typeof(Good))]
    public class GoodListDto : EntityDto<Guid>, IHasCreationTime, ICreationAudited
    {

        
		/// <summary>
		/// Specification
		/// </summary>
		[Required(ErrorMessage="Specification不能为空")]
		public string Specification { get; set; }



		/// <summary>
		/// PhotoUrl
		/// </summary>
		public string PhotoUrl { get; set; }



		/// <summary>
		/// Desc
		/// </summary>
		public string Desc { get; set; }



		/// <summary>
		/// Stock
		/// </summary>
		public decimal? Stock { get; set; }



		/// <summary>
		/// Unit
		/// </summary>
		public string Unit { get; set; }



		/// <summary>
		/// ExchangeCode
		/// </summary>
		public string ExchangeCode { get; set; }



		/// <summary>
		/// CategoryId
		/// </summary>
		public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        /// <summary>
        /// Integral
        /// </summary>
        public decimal? Integral { get; set; }



		/// <summary>
		/// BarCode
		/// </summary>
		public string BarCode { get; set; }



		/// <summary>
		/// SearchCount
		/// </summary>
		public int? SellCount { get; set; }



		/// <summary>
		/// IsAction
		/// </summary>
		public bool? IsAction { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }

        public DateTime OnlineTime { get; set; }
        public DateTime OfflineTime { get; set; }

        /// <summary>
        /// CreatorUserId
        /// </summary>
        public long? CreatorUserId { get; set; }

        public bool IsBanner { get; set; }

        public string BannerUrl { get; set; }
    }
}