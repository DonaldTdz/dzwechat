

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Orders;
using static HC.DZWechat.DZEnums.DZEnums;

namespace HC.DZWechat.Orders.Dtos
{
    public class OrderListDto : EntityDto<Guid>,IHasCreationTime,ICreationAudited 
    {

        
		/// <summary>
		/// Number
		/// </summary>
		[Required(ErrorMessage="Number不能为空")]
		public string Number { get; set; }



		/// <summary>
		/// UserId
		/// </summary>
		public Guid? UserId { get; set; }



		/// <summary>
		/// Phone
		/// </summary>
		public string Phone { get; set; }



		/// <summary>
		/// Integral
		/// </summary>
		public decimal? Integral { get; set; }



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

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }



		/// <summary>
		/// PayTime
		/// </summary>
		public DateTime? PayTime { get; set; }



		/// <summary>
		/// CompleteTime
		/// </summary>
		public DateTime? CompleteTime { get; set; }



		/// <summary>
		/// CancelTime
		/// </summary>
		public DateTime? CancelTime { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// CreatorUserId
		/// </summary>
		public long? CreatorUserId { get; set; }



		/// <summary>
		/// DeliveryName
		/// </summary>
		public string DeliveryName { get; set; }



		/// <summary>
		/// DeliveryPhone
		/// </summary>
		public string DeliveryPhone { get; set; }



		/// <summary>
		/// DeliveryAddress
		/// </summary>
		public string DeliveryAddress { get; set; }




    }
}