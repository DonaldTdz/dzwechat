using Abp.Application.Services;
using HC.DZWechat.Dtos;
using HC.DZWechat.Goods.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.GoodsWechat
{
    public interface IGoodsWechatAppService : IApplicationService
    {
        Task<WxPagedResultDto<GoodsGridDto>> GetHeatGoodsAsync(WxPagedInputDto input);

        Task<WxPagedResultDto<GoodsGridDto>> GetSearchGoodsAsync(GoodsSearchInputDto input);

        Task<List<GoodsGridDto>> GetGroupGoodsAsync(int groupId, int top);

        Task<GoodsDetailDto> GetGoodsDetailAsync(Guid id);

        Task<List<GoodListDto>> GetGoodsBanner();
    }
}
