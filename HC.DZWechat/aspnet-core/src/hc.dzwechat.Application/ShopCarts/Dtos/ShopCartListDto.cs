

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.ShopCarts;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.ShopCarts.Dtos
{
    public class ShopCartListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// UserId
		/// </summary>
		public Guid? UserId { get; set; }

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
		/// ExchangeCode
		/// </summary>
		public ExchangeCode ExchangeCode { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }




    }
}