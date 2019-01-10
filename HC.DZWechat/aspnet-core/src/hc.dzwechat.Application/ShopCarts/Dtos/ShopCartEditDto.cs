
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.ShopCarts;

namespace  HC.DZWechat.ShopCarts.Dtos
{
    public class ShopCartEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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

    public class ShopCartInputDto
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string WxOpenId { get; set; }


        /// <summary>
        /// GoodsId
        /// </summary>
        [Required(ErrorMessage = "GoodsId不能为空")]
        public Guid GoodsId { get; set; }


        /// <summary>
        /// Num
        /// </summary>
        public decimal? Num { get; set; }



        /// <summary>
        /// ExchangeCode
        /// </summary>
        public ExchangeCode ExchangeCode { get; set; }

    }
}