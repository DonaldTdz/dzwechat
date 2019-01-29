using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.GoodsWechat.Dtos
{
    [AutoMapFrom(typeof(ShopGoods))]
    public class GoodsCacheItem : EntityDto<Guid>, IHasCreationTime, ICreationAudited
    {
        public string Specification { get; set; }

        public string PhotoUrl { get; set; }

        public string Desc { get; set; }

        public decimal? Stock { get; set; }

        public string Unit { get; set; }

        public string ExchangeCode { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal? Integral { get; set; }

        public string BarCode { get; set; }

        public decimal? SellCount { get; set; }

        public bool? IsAction { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? OnlineTime { get; set; }

        public DateTime? OfflineTime { get; set; }

        public long? CreatorUserId { get; set; }

        public bool IsBanner { get; set; }

        public string BannerUrl { get; set; }
    }
}
