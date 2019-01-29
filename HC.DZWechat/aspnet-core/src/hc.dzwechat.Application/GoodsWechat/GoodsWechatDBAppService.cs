using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using HC.DZWechat.Categorys;
using HC.DZWechat.Configuration;
using HC.DZWechat.Dtos;
using HC.DZWechat.Goods;
using HC.DZWechat.Goods.DomainService;
using HC.DZWechat.Goods.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HC.DZWechat.GoodsWechat
{
    public class GoodsWechatDBAppService : DZWechatAppServiceBase, IGoodsWechatAppService
    {
        private readonly IRepository<ShopGoods, Guid> _entityRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IGoodManager _entityManager;
        private readonly IConfigurationRoot _appConfiguration;

        private string _hostUrl;

        public GoodsWechatDBAppService(
        IRepository<ShopGoods, Guid> entityRepository
        , IRepository<Category> categoryRepository
        , IGoodManager entityManager
        , IHostingEnvironment env
            )
        {
            _entityRepository = entityRepository;
            _categoryRepository = categoryRepository;
            _entityManager = entityManager;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
            _hostUrl = _appConfiguration["App:ServerRootAddress"];
        }

        [AbpAllowAnonymous]
        public async Task<List<GoodListDto>> GetGoodsBanner()
        {
            var result = await _entityRepository.GetAll().Where(v => v.BannerUrl != null).Select(v => new GoodListDto()
            {
                Id = v.Id,
                BannerUrl = _hostUrl + v.BannerUrl,
                CreationTime = v.CreationTime
            }).OrderByDescending(v => v.CreationTime).Take(5).ToListAsync();
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<GoodsDetailDto> GetGoodsDetailAsync(Guid id)
        {
            var query = await _entityRepository.GetAsync(id);
            var cat = await _categoryRepository.GetAsync(query.CategoryId.Value);
            var result = query.MapTo<GoodsDetailDto>();
            result.Host = _hostUrl;
            result.CategoryName = cat.Name;
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<List<GoodsGridDto>> GetGroupGoodsAsync(int groupId, int top)
        {
            top = top == 0 ? 50 : top;
            var goodsList = await _entityRepository.GetAll()
                .Where(e => e.IsAction == true && e.Stock != 0 && e.CategoryId == groupId)
                .OrderByDescending(o => o.CreationTime)
                .Take(top)
                .Select(e => new GoodsGridDto(_hostUrl)
                {
                    Id = e.Id,
                    name = e.Specification,
                    saleCount = e.SellCount ?? 0,
                    price = e.Integral,
                    photoUrl = e.PhotoUrl,
                    unit = e.Unit
                }).ToListAsync();
            return goodsList;
        }

        [AbpAllowAnonymous]
        public async Task<WxPagedResultDto<GoodsGridDto>> GetHeatGoodsAsync(WxPagedInputDto input)
        {
            var query = _entityRepository.GetAll().Where(e => e.IsAction == true && e.Stock != 0).Select(e => new GoodsGridDto(_hostUrl)
            {
                Id = e.Id,
                name = e.Specification,
                saleCount = e.SellCount ?? 0,
                price = e.Integral,
                photoUrl = e.PhotoUrl,
                unit = e.Unit
            });
            var count = await query.CountAsync();
            var dataList = await query.OrderByDescending(o => o.saleCount)
                        .PageBy(input)
                        .ToListAsync();
            var result = new WxPagedResultDto<GoodsGridDto>(count, dataList);
            result.PageSize = input.Size;
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<WxPagedResultDto<GoodsGridDto>> GetSearchGoodsAsync(GoodsSearchInputDto input)
        {
            var newData = DateTime.Today.AddDays(-60);//近60天上新的产品
            var query = _entityRepository.GetAll().Where(e => e.IsAction == true && e.Stock != 0)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Specification.Contains(input.KeyWord) || e.Desc.Contains(input.KeyWord))
                .WhereIf(input.CategoryId != 0, e => e.CategoryId == input.CategoryId)
                .WhereIf(input.CateCode == "new", e => e.CreationTime >= newData);
            if (input.CateCode == "new")
            {
                input.SortType = SortTypeEnum.News;
            }
            switch (input.SortType)
            {
                case SortTypeEnum.None:
                    {
                        query = query.OrderByDescending(q => q.SellCount).OrderBy(q => q.Integral);
                    }
                    break;
                case SortTypeEnum.ZongHe:
                    {
                        query = query.OrderByDescending(q => q.SellCount).OrderBy(q => q.Integral);
                    }
                    break;
                case SortTypeEnum.Sale:
                    {
                        query = query.OrderByDescending(q => q.SellCount);
                    }
                    break;
                case SortTypeEnum.PriceAsc:
                    {
                        query = query.OrderBy(q => q.Integral);
                    }
                    break;
                case SortTypeEnum.PriceDesc:
                    {
                        query = query.OrderByDescending(q => q.Integral);
                    }
                    break;
                case SortTypeEnum.News:
                    {
                        query = query.OrderByDescending(q => q.CreationTime);
                    }
                    break;
            }
            var selectQuery = query.Select(e => new GoodsGridDto(_hostUrl)
            {
                Id = e.Id,
                name = e.Specification,
                saleCount = e.SellCount ?? 0,
                price = e.Integral,
                photoUrl = e.PhotoUrl,
                unit = e.Unit
            });
            var count = await selectQuery.CountAsync();
            var dataList = await selectQuery.PageBy(input)
                        .ToListAsync();
            var result = new WxPagedResultDto<GoodsGridDto>(count, dataList);
            result.PageSize = input.Size;
            return result;
        }
    }
}
