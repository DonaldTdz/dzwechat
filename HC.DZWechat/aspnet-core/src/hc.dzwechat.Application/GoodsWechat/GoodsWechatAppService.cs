using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using HC.DZWechat.Categorys;
using HC.DZWechat.Configuration;
using HC.DZWechat.Dtos;
using HC.DZWechat.Goods.Dtos;
using HC.DZWechat.GoodsWechat.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HC.DZWechat.GoodsWechat
{
    public class GoodsWechatAppService : DZWechatAppServiceBase, IGoodsWechatAppService, ITransientDependency
    {
        private readonly IGoodsCache _goodsCache;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IConfigurationRoot _appConfiguration;
        private string _hostUrl;

        public GoodsWechatAppService(IGoodsCache goodsCache, IRepository<Category> categoryRepository,  IHostingEnvironment env)
        {
            _goodsCache = goodsCache;
            _categoryRepository = categoryRepository;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
            _hostUrl = _appConfiguration["App:ServerRootAddress"];
        }

        [AbpAllowAnonymous]
        public async Task<List<GoodListDto>> GetGoodsBanner()
        {
            var query = await _goodsCache.GetAllAsync();
            var result = query.Where(v => v.BannerUrl != null).Select(v => new GoodListDto()
            {
                Id = v.Id,
                BannerUrl = _hostUrl + v.BannerUrl,
                CreationTime = v.CreationTime
            }).OrderByDescending(v => v.CreationTime).Take(5).ToList();
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<GoodsDetailDto> GetGoodsDetailAsync(Guid id)
        {
            var goods = _goodsCache[id];
            var cat = await _categoryRepository.GetAsync(goods.CategoryId.Value);
            var result = goods.MapTo<GoodsDetailDto>();
            result.Host = _hostUrl;
            result.CategoryName = cat.Name;
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<List<GoodsGridDto>> GetGroupGoodsAsync(int groupId, int top)
        {
            top = top == 0 ? 50 : top;
            var query = await _goodsCache.GetAllAsync();
            var goodsList = query.Where(e => e.IsAction == true && e.Stock != 0 && e.CategoryId == groupId)
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
                }).ToList();
            return goodsList;
        }

        [AbpAllowAnonymous]
        public async Task<WxPagedResultDto<GoodsGridDto>> GetHeatGoodsAsync(WxPagedInputDto input)
        {
            var queryList = await _goodsCache.GetAllAsync();
            var query = queryList.Where(e => e.IsAction == true && e.Stock != 0).Select(e => new GoodsGridDto(_hostUrl)
            {
                Id = e.Id,
                name = e.Specification,
                saleCount = e.SellCount ?? 0,
                price = e.Integral,
                photoUrl = e.PhotoUrl,
                unit = e.Unit
            });
            var count = query.Count();
            var dataList = query.OrderByDescending(o => o.saleCount)
                        .Skip(input.SkipCount).Take(input.Size)
                        .ToList();
            var result = new WxPagedResultDto<GoodsGridDto>(count, dataList);
            result.PageSize = input.Size;
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<WxPagedResultDto<GoodsGridDto>> GetSearchGoodsAsync(GoodsSearchInputDto input)
        {
            var newData = DateTime.Today.AddDays(-60);//近60天上新的产品
            var queryList = await _goodsCache.GetAllAsync();
            var query = queryList.Where(e => e.IsAction == true && e.Stock != 0);

            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(e => e.Specification.Contains(input.KeyWord));// || e.Desc.Contains(input.KeyWord)
            }

            if (input.CategoryId != 0)
            {
                query = query.Where(e => e.CategoryId == input.CategoryId);
            }

            if (input.CateCode == "new")
            {
                query = query.Where(e => e.CreationTime >= newData);
            }
                
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
            }).ToList();
            var count = selectQuery.Count();
            var dataList = selectQuery.Skip(input.SkipCount).Take(input.Size).ToList();
            var result = new WxPagedResultDto<GoodsGridDto>(count, dataList);
            result.PageSize = input.Size;
            return result;
        }
    }
}
