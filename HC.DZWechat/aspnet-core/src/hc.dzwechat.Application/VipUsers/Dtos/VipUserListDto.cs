

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.VipUsers;

namespace HC.DZWechat.VipUsers.Dtos
{
    public class VipUserListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// VipCode
		/// </summary>
		public string VipCode { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// Phone
		/// </summary>
		public string Phone { get; set; }



		/// <summary>
		/// IdNumber
		/// </summary>
		public string IdNumber { get; set; }



		/// <summary>
		/// PurchaseAmount
		/// </summary>
		public decimal? PurchaseAmount { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }

        public string BindWechatUser { get; set; }
        public Guid? WechatId { get; set; }
    }
}