
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.OrderDetails;

namespace  HC.DZWechat.OrderDetails.Dtos
{
    public class OrderDetailEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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
		public int? Status { get; set; }



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