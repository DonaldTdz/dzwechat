

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
    }

    public class GoodsGridDto : EntityDto<Guid>
    {
        public GoodsGridDto() { }

        public GoodsGridDto(string _host)
        {
            host = _host;
        }

        [NonSerialized]
        public string photoUrl;

        [NonSerialized]
        public string host;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string name { get; set; }

        //public string PhotoUrl { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 所需积分
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string thumLogo
        {
            get
            {
                var arr = photoUrl.Split(',');
                if (arr.Length > 0)
                {
                    return host + arr[0];
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 销量
        /// </summary>
        public int? saleCount { get; set; }
    }
}