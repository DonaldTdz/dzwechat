

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.ShopCarts;
using HC.DZWechat.DZEnums.DZCommonEnums;
using System.Collections.Generic;

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
		public ExchangeCodeEnum ExchangeCode { get; set; }


		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }

    }

    public class UserCartDto : EntityDto<Guid>
    {
        public UserCartDto()
        {
            ischecked = true;
        }

        public UserCartDto(string host)
        {
            Host = host;
            ischecked = true;
        }

        /// <summary>
        /// UserId
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// GoodsId
        /// </summary>
        public Guid GoodsId { get; set; }

        /// <summary>
        /// Specification
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// Integral
        /// </summary>
        public decimal? Integral { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; }

        private decimal? num;

        /// <summary>
        /// Num
        /// </summary>
        public decimal? Num
        {
            get
            {
                if (num > Stock)
                {
                    return Stock;
                }
                return num;
            }
            set
            {
                num = value;
            }
        }

        /// <summary>
        /// ExchangeCode
        /// </summary>
        public ExchangeCodeEnum ExchangeCode { get; set; }

        public string ExchangeCodeDesc
        {
            get
            {
                return ExchangeCode.ToString();
            }
        }

        [NonSerialized]
        public string PhotoUrl;

        [NonSerialized]
        public string Host;

        /// <summary>
        /// 封面
        /// </summary>
        public string ThumLogo
        {
            get
            {
                if (!string.IsNullOrEmpty(PhotoUrl))
                {
                    var arr = PhotoUrl.Split(',');
                    if (arr.Length > 0)
                    {
                        return Host + arr[0];
                    }
                }

                return string.Empty;
            }
        }

        public string CategoryName { get; set; }

        public decimal? Stock { get; set; }

        public bool? IsAction { get; set; }

        private bool? ischecked;

        public bool? Ischecked
        {
            get
            {
                if (IsAction == false)
                {
                    return false;
                }
                return ischecked;
            }
            set
            {
                ischecked = value;
            }
        }

        public decimal? PriceSubtotal
        {
            get
            {
                return Integral * Num;
            }
        }

    }

    public class UserCart
    {
        public List<UserCartDto> Items { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}