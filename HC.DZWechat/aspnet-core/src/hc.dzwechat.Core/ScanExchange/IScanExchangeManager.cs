

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.CommonDto;
using HC.DZWechat.Orders;
using HC.DZWechat.ScanExchange.Dtos;

namespace HC.DZWechat.ScanExchange
{
    public interface IScanExchangeManager : IDomainService
    {
        Task<bool> GetIsAttentionByIdAsync(string openId);
        Task<bool> IsShopManagerByIdAsync(string openId);
        Task<List<OrderDetailDto>> GetExchangeGoodsByIdAsync(Guid orderId, string openId);
        Task<APIResultDto> ExchangeGoods(Guid orderId);
    }
}
