

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.VipPurchases;

namespace HC.DZWechat.VipPurchases.Dtos
{
    public class VipPurchaseListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// VipUserId
		/// </summary>
		[Required(ErrorMessage="VipUserId不能为空")]
		public Guid VipUserId { get; set; }



		/// <summary>
		/// VipCode
		/// </summary>
		public string VipCode { get; set; }



		/// <summary>
		/// PurchaseAmount
		/// </summary>
		public decimal? PurchaseAmount { get; set; }



		/// <summary>
		/// IsConvert
		/// </summary>
		public bool? IsConvert { get; set; }



		/// <summary>
		/// ConvertTime
		/// </summary>
		public DateTime? ConvertTime { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}