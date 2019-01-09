
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using HC.DZWechat.Goods;
using HC.DZWechat.Goods.Dtos;
using HC.DZWechat.Goods.DomainService;
using HC.DZWechat.Categorys;
using HC.DZWechat.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using HC.DZWechat.Configuration;

namespace HC.DZWechat.Goods
{
    /// <summary>
    /// Good应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class GoodAppService : DZWechatAppServiceBase, IGoodAppService
    {
        private readonly IRepository<Good, Guid> _entityRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IGoodManager _entityManager;
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public GoodAppService(
        IRepository<Good, Guid> entityRepository
        , IRepository<Category> categoryRepository
        , IGoodManager entityManager
        , IHostingEnvironment env
        )
        {
            _entityRepository = entityRepository;
            _categoryRepository = categoryRepository;
            _entityManager = entityManager;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }


        /// <summary>
        /// 获取Good的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<GoodListDto>> GetPaged(GetGoodsInput input)
        {
            if (input.NodeKey == "root")
            {
                var goods = _entityRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.Filter), r => r.Specification.Contains(input.Filter) || r.BarCode.Contains(input.Filter));
                var category = _categoryRepository.GetAll();
                var query = from g in goods
                            join c in category on g.CategoryId equals c.Id
                            select new GoodListDto()
                            {
                                Id = g.Id,
                                Specification = g.Specification,
                                Unit = g.Unit,
                                CategoryName = c.Name,
                                PhotoUrl = g.PhotoUrl,
                                //Stock = g.Stock,
                                //BarCode = g.BarCode,
                                //Desc = g.Desc,
                                //ExchangeCode = g.ExchangeCode,
                                OnlineTime = g.OnlineTime,
                                Integral = g.Integral,
                                IsAction = g.IsAction,
                                SellCount = g.SellCount
                            };
                var count = await query.CountAsync();
                var entityList = await query
                        .OrderBy(input.Sorting).AsNoTracking()
                        .PageBy(input)
                        .ToListAsync();
                var entityListDtos = entityList.MapTo<List<GoodListDto>>();
                return new PagedResultDto<GoodListDto>(count, entityListDtos);
            }
            else
            {
                var goods = _entityRepository.GetAll().Where(v => v.CategoryId == Convert.ToInt32(input.NodeKey))
                    .WhereIf(!string.IsNullOrEmpty(input.Filter), r => r.Specification.Contains(input.Filter) || r.BarCode.Contains(input.Filter));
                var category = _categoryRepository.GetAll();
                var query = from g in goods
                            join c in category on g.CategoryId equals c.Id
                            select new GoodListDto()
                            {
                                Id = g.Id,
                                Specification = g.Specification,
                                Unit = g.Unit,
                                CategoryName = c.Name,
                                PhotoUrl = g.PhotoUrl,
                                //Stock = g.Stock,
                                //BarCode = g.BarCode,
                                //Desc = g.Desc,
                                //ExchangeCode = g.ExchangeCode,
                                OnlineTime = g.OnlineTime,
                                Integral = g.Integral,
                                IsAction = g.IsAction,
                                SellCount = g.SellCount
                            };
                var count = await query.CountAsync();

                var entityList = await query
                        .OrderBy(input.Sorting).AsNoTracking()
                        .PageBy(input)
                        .ToListAsync();
                var entityListDtos = entityList.MapTo<List<GoodListDto>>();
                return new PagedResultDto<GoodListDto>(count, entityListDtos);
            }
        }


        /// <summary>
        /// 通过指定id获取GoodListDto信息
        /// </summary>

        public async Task<GoodListDto> GetById(Guid id)
        {
            var entity = await _entityRepository.GetAsync(id);
            return entity.MapTo<GoodListDto>();
        }

        /// <summary>
        /// 获取编辑 Good
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetGoodForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetGoodForEditOutput();
            GoodEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<GoodEditDto>();

                //goodEditDto = ObjectMapper.Map<List<goodEditDto>>(entity);
            }
            else
            {
                editDto = new GoodEditDto();
            }

            output.Good = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Good的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GoodListDto> CreateOrUpdate(GoodEditDto input)
        {

            if (input.Id.HasValue)
            {
                return await Update(input);
            }
            else
            {
                return await Create(input);
            }
        }


        /// <summary>
        /// 新增Good
        /// </summary>
        protected virtual async Task<GoodListDto> Create(GoodEditDto input)
        {
            input.SellCount = 0;
            var entity = input.MapTo<Good>();
            entity = await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<GoodListDto>();
        }

        /// <summary>
        /// 编辑Good
        /// </summary>

        protected virtual async Task<GoodListDto> Update(GoodEditDto input)
        {
            if (input.IsAction == true)
            {
                input.OnlineTime = DateTime.Now;
            }
            else
            {
                input.OfflineTime = DateTime.Now;
            }
            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            await _entityRepository.UpdateAsync(entity);
            return entity.MapTo<GoodListDto>();
        }



        /// <summary>
        /// 删除Good信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }


        /// <summary>
        /// 批量删除Good的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 商品上架or下架
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GoodListDto> ChangeStatus(GoodEditDto input)
        {
            var entity = await _entityRepository.GetAsync(input.Id.Value);
            entity.IsAction = input.IsAction;
            await _entityRepository.UpdateAsync(entity);
            return entity.MapTo<GoodListDto>();
        }


        /// <summary>
        /// 热卖商品
        /// </summary>
        [AbpAllowAnonymous]
        public async Task<WxPagedResultDto<GoodsGridDto>> GetHeatGoodsAsync(WxPagedInputDto input)
        {
            var hostUrl = _appConfiguration["App:ServerRootAddress"];
            var query = _entityRepository.GetAll().Where(e => e.IsAction == true).Select(e => new GoodsGridDto(hostUrl)
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

        /// <summary>
        /// 商品搜索
        /// </summary>
        [AbpAllowAnonymous]
        public async Task<WxPagedResultDto<GoodsGridDto>> GetSearchGoodsAsync(GoodsSearchInputDto input)
        {
            var hostUrl = _appConfiguration["App:ServerRootAddress"];
            var query = _entityRepository.GetAll().Where(e => e.IsAction == true)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Specification.Contains(input.KeyWord) || e.Desc.Contains(input.KeyWord))
                .WhereIf(input.CategoryId != 0, e => e.CategoryId == input.CategoryId);

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
            }

            var selectQuery = query.Select(e => new GoodsGridDto(hostUrl)
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

        [AbpAllowAnonymous]
        public async Task<List<GoodsGridDto>> GetGroupGoodsAsync(int groupId, int top)
        {
            top = top == 0 ? 50 : top;
            var hostUrl = _appConfiguration["App:ServerRootAddress"];
            var goodsList = await _entityRepository.GetAll()
                .Where(e => e.IsAction == true && e.CategoryId == groupId)
                .OrderByDescending(o => o.CreationTime)
                .Take(top)
                .Select(e => new GoodsGridDto(hostUrl)
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
    }
}


