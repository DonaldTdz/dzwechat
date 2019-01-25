
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


using HC.DZWechat.OrderDetails;
using HC.DZWechat.OrderDetails.Dtos;
using HC.DZWechat.OrderDetails.DomainService;
using HC.DZWechat.Goods;
using HC.DZWechat.DZEnums.DZCommonEnums;
using Microsoft.Extensions.Configuration;
using HC.DZWechat.Configuration;
using Microsoft.AspNetCore.Hosting;
using HC.DZWechat.Exchanges;
using HC.DZWechat.Shops;

namespace HC.DZWechat.OrderDetails
{
    /// <summary>
    /// OrderDetail应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class OrderDetailAppService : DZWechatAppServiceBase, IOrderDetailAppService
    {
        private readonly IRepository<OrderDetail, Guid> _entityRepository;
        private readonly IRepository<Goods.ShopGoods, Guid> _goodRepository;
        private readonly IRepository<Exchange, Guid> _exchangeRepository;
        private readonly IRepository<Shop, Guid> _shopRepository;
        private readonly IOrderDetailManager _entityManager;
        private readonly IConfigurationRoot _appConfiguration;
        private string _hostUrl;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public OrderDetailAppService(
        IRepository<OrderDetail, Guid> entityRepository
        , IRepository<Goods.ShopGoods, Guid> goodRepository
        , IOrderDetailManager entityManager
            , IRepository<Shop, Guid> shopRepository
            , IRepository<Exchange, Guid> exchangeRepository
             , IHostingEnvironment env
        )
        {
            _entityRepository = entityRepository;
            _goodRepository = goodRepository;
            _entityManager = entityManager;
            _exchangeRepository = exchangeRepository;
            _shopRepository = shopRepository;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
            _hostUrl = _appConfiguration["App:ServerRootAddress"];

        }


        /// <summary>
        /// 获取OrderDetail的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<OrderDetailListDto>> GetPaged(GetOrderDetailsInput input)
        {
            var exchange = _exchangeRepository.GetAll();
            var orderDetail = _entityRepository.GetAll().Where(v => v.OrderId == input.OrderId);
            var shop = _shopRepository.GetAll();
            var queryEO = from e in exchange
                          join s in shop on e.ShopId equals s.Id
                          select new OrderDetailListDto()
                          {
                              ShopName = s.Name,
                              //ExchangeCode = e.ExchangeCode,
                              OrderDetailId = e.OrderDetailId
                          };
            var result = from od in orderDetail
                         join q in queryEO on od.Id equals q.OrderDetailId into table
                         from t in table.DefaultIfEmpty()
                         select new OrderDetailListDto()
                         {
                             Id = od.Id,
                             ShopName = t.ShopName,
                             ExchangeCode = od.ExchangeCode,
                             CreationTime = od.CreationTime,
                             ExchangeTime = od.ExchangeTime,
                             Specification = od.Specification,
                             Num = od.Num,
                             Unit = od.Unit,
                             Integral = od.Integral,
                             Status = od.Status
                         };
            var count = await result.CountAsync();
            var entityList = await result
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            // var entityListDtos = ObjectMapper.Map<List<OrderDetailListDto>>(entityList);
            //var entityListDtos = entityList.MapTo<List<OrderDetailListDto>>();

            return new PagedResultDto<OrderDetailListDto>(count, entityList);
        }


        /// <summary>
        /// 通过指定id获取OrderDetailListDto信息
        /// </summary>

        public async Task<OrderDetailListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<OrderDetailListDto>();
        }

        /// <summary>
        /// 获取编辑 OrderDetail
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetOrderDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetOrderDetailForEditOutput();
            OrderDetailEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<OrderDetailEditDto>();

                //orderDetailEditDto = ObjectMapper.Map<List<orderDetailEditDto>>(entity);
            }
            else
            {
                editDto = new OrderDetailEditDto();
            }

            output.OrderDetail = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改OrderDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateOrderDetailInput input)
        {

            if (input.OrderDetail.Id.HasValue)
            {
                await Update(input.OrderDetail);
            }
            else
            {
                await Create(input.OrderDetail);
            }
        }


        /// <summary>
        /// 新增OrderDetail
        /// </summary>

        protected virtual async Task<OrderDetailEditDto> Create(OrderDetailEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <OrderDetail>(input);
            var entity = input.MapTo<OrderDetail>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<OrderDetailEditDto>();
        }

        /// <summary>
        /// 编辑OrderDetail
        /// </summary>

        protected virtual async Task Update(OrderDetailEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除OrderDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除OrderDetail的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<WXOrderDetailListWitStatusDto> GetOrderDetailListByIdAsync(GetOrderDetailsInput input)
        {
            var orderDetail = _entityRepository.GetAll().Where(v => v.OrderId == input.OrderId);
            var goods = _goodRepository.GetAll();
            var exchanges = _exchangeRepository.GetAll();
            var result = await (from od in orderDetail
                                join g in goods on od.GoodsId equals g.Id
                                join e in exchanges on od.Id equals e.OrderDetailId into table
                                from t in table.DefaultIfEmpty()
                                select new WXOrderDetailListDto()
                                {
                                    Id = od.Id,
                                    Specification = od.Specification,
                                    Unit = od.Unit,
                                    Num = od.Num,
                                    CreationTime = od.CreationTime,
                                    ExchangeCode = od.ExchangeCode,
                                    ExchangeTime = od.ExchangeTime,
                                    Integral = od.Integral,
                                    Status = od.Status,
                                    PhotoUrl = _hostUrl + g.PhotoUrl,
                                    LogisticsCompany = t.LogisticsCompany ?? null,
                                    LogisticsNo = t.LogisticsNo ?? null
                                }).OrderByDescending(v => v.CreationTime).AsNoTracking().ToListAsync();
            foreach (var item in result)
            {
                if (item.Status == ExchangeStatus.已兑换)
                {
                    return new WXOrderDetailListWitStatusDto()
                    {
                        List = result,
                        IsCancel = false
                    };
                }
            }
            return new WXOrderDetailListWitStatusDto()
            {
                List = result,
                IsCancel = true
            };
        }
    }
}


