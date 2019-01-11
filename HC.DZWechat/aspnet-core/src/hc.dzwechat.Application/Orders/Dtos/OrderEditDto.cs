
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Orders;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace  HC.DZWechat.Orders.Dtos
{
    public class OrderEditDto :IHasCreationTime, ICreationAudited
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

        public string NickName { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        //[Required(ErrorMessage="Number不能为空")]
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