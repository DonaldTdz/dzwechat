

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Goods;
using Abp.AutoMapper;

namespace HC.DZWechat.Goods.Dtos
{
    [AutoMapFrom(typeof(ShopGoods))]
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
		public decimal? SellCount { get; set; }



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

        public bool IsBanner { get; set; }

        public string BannerUrl { get; set; }
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
        /// 图片
        /// </summary>
        public string thumLogo
        {
            get
            {
                if (!string.IsNullOrEmpty(photoUrl))
                {
                    var arr = photoUrl.Split(',');
                    if (arr.Length > 0)
                    {
                        return host + arr[0];
                    }
                }
               
                return string.Empty;
            }
        }

        /// <summary>
        /// 销量
        /// </summary>
        public decimal? saleCount { get; set; } 
    }


    /// <summary>
    /// 统计
    /// </summary>
    public class IntegralStatisDto
    {
        public string GroupName { get; set; }
        public decimal? IntegralTotal { get; set; }
        public decimal? Total { get; set; }
    }

    [AutoMapFrom(typeof(ShopGoods))]
    public class GoodsDetailDto : EntityDto<Guid>
    {
        public GoodsDetailDto() { }

        public GoodsDetailDto(string _host)
        {
            Host = _host;
        }

        [NonSerialized]
        public string PhotoUrl;

        [NonSerialized]
        public string Host;

        [NonSerialized]
        public string ExchangeCode;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 所需积分
        /// </summary>
        public decimal? Integral { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string[] PhotoList
        {
            get
            {
                if (!string.IsNullOrEmpty(PhotoUrl))
                {
                    var arr = PhotoUrl.Split(',');
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arr[i] = Host + arr[i];
                        }
                    }
                    return arr;
                }
                return null;
            }
        }

        /// <summary>
        /// 销量
        /// </summary>
        public decimal? SellCount { get; set; }

        public string Desc { get; set; }

        public string[] ExchangeList
        {
            get
            {
                if (!string.IsNullOrEmpty(ExchangeCode))
                {
                    var arr = ExchangeCode.Split(',');
                    string[] exchangearr = new string[arr.Length];
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (arr[i] == "1")
                            {
                                exchangearr[i] = "线下兑换";
                            } else if (arr[i] == "2")
                            {
                                exchangearr[i] = "邮寄兑换";
                            }
                        }
                    }
                    return exchangearr;
                }
                return null;
            }
        }

        public string CategoryName { get; set; }

        public decimal? Stock { get; set; }

        public bool? IsAction { get; set; }

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
    }
}