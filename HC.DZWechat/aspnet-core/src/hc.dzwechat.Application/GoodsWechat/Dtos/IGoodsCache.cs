using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.GoodsWechat.Dtos
{
    public interface IGoodsCache : IEntityCache<GoodsCacheItem, Guid>
    {
        Task<List<GoodsCacheItem>> GetAllAsync();

        Task RemoveAllGoodsAsync();
    }
}
