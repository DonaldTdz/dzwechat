
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.VipUsers;

namespace  HC.DZWechat.VipUsers.Dtos
{
    public class VipUserEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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




    }
}