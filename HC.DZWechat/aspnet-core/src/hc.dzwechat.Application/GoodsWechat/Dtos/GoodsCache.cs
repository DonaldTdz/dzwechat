using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using HC.DZWechat.Goods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.GoodsWechat.Dtos
{
    public class GoodsCache : EntityCache<ShopGoods, GoodsCacheItem, Guid>, IGoodsCache, ITransientDependency
    {
        public GoodsCache(ICacheManager cacheManager, IRepository<ShopGoods, Guid> repository)
       : base(cacheManager, repository)
        {

        }

        public async Task<List<GoodsCacheItem>> GetAllAsync()
        {
            var dataList = await CacheManager.GetCache(CacheName).GetAsync("AllGoods", () => Repository.GetAllListAsync());
            return dataList.MapTo<List<GoodsCacheItem>>();
        }
    }
}
