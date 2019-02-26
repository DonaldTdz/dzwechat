using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using HC.DZWechat.Goods;
using HC.DZWechat.GoodsWechat.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.GoodsWechat.EventHandler
{
    public class GoodsEventHandler
        : IEventHandler<EntityChangedEventData<ShopGoods>>
        , ITransientDependency
    {
        private readonly IGoodsCache _goodsCache;

        public GoodsEventHandler(IGoodsCache goodsCache)
        {
            _goodsCache = goodsCache;
        }

        public void HandleEvent(EntityChangedEventData<ShopGoods> eventData)
        {
            AsyncHelper.RunSync(async () =>
            {
                await _goodsCache.RemoveAllGoodsAsync();
            });
        }
    }
}
