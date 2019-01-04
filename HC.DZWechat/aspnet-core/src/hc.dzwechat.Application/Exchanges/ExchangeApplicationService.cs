
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


using HC.DZWechat.Exchanges;
using HC.DZWechat.Exchanges.Dtos;
using HC.DZWechat.Exchanges.DomainService;
using HC.DZWechat.OrderDetails;
using HC.DZWechat.Orders;

namespace HC.DZWechat.Exchanges
{
    /// <summary>
    /// Exchange应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ExchangeAppService : DZWechatAppServiceBase, IExchangeAppService
    {
        private readonly IRepository<Exchange, Guid> _entityRepository;
        private readonly IRepository<OrderDetail, Guid> _orderDetailRepository;
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IExchangeManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ExchangeAppService(
        IRepository<Exchange, Guid> entityRepository
        , IRepository<OrderDetail, Guid> orderDetailRepository
                    , IRepository<Order, Guid> orderRepository
        , IExchangeManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取Exchange的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ExchangeListDto>> GetPaged(GetExchangesInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ExchangeListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ExchangeListDto>>();

            return new PagedResultDto<ExchangeListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取ExchangeListDto信息
        /// </summary>

        public async Task<ExchangeListDto> GetById(Guid id)
        {
            var entity = await _entityRepository.GetAll().Where(v => v.OrderDetailId == id).FirstOrDefaultAsync();
            return entity.MapTo<ExchangeListDto>();
        }

        /// <summary>
        /// 获取编辑 Exchange
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetExchangeForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetExchangeForEditOutput();
            ExchangeEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ExchangeEditDto>();

                //exchangeEditDto = ObjectMapper.Map<List<exchangeEditDto>>(entity);
            }
            else
            {
                editDto = new ExchangeEditDto();
            }

            output.Exchange = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Exchange的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<ExchangeListDto> CreateOrUpdate(ExchangeEditDto input)
        {
            input.UserId = AbpSession.UserId;
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
        /// 新增Exchange
        /// </summary>
        protected virtual async Task<ExchangeListDto> Create(ExchangeEditDto input)
        {
            var entity = input.MapTo<Exchange>();
            entity = await _entityRepository.InsertAsync(entity);
            await UpdateOrderDetailStatus(input.OrderDetailId);
            Guid orderId = await GetOrderId(input.OrderDetailId);
            await CurrentUnitOfWork.SaveChangesAsync();
            bool isOk = await CheckedOrderStatus(orderId);
            if (isOk == true)
            {
                await UpdateOrderStatus(orderId);
            }
            return entity.MapTo<ExchangeListDto>();
        }


        /// <summary>
        /// 编辑Exchange
        /// </summary>
        protected virtual async Task<ExchangeListDto> Update(ExchangeEditDto input)
        {
            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            await _entityRepository.UpdateAsync(entity);
            await UpdateOrderDetailStatus(input.OrderDetailId);
            Guid orderId = await GetOrderId(input.OrderDetailId);
            await CurrentUnitOfWork.SaveChangesAsync();
            bool isOk = await CheckedOrderStatus(orderId);
            if (isOk == true)
            {
                await UpdateOrderStatus(orderId);
            }
            return entity.MapTo<ExchangeListDto>();
        }


        /// <summary>
        /// 删除Exchange信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Exchange的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 更新订单明细状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task UpdateOrderDetailStatus(Guid? id)
        {
            var entity = await _orderDetailRepository.GetAsync(id.Value);
            entity.Status = DZEnums.DZEnums.ExchangeStatus.已兑换;
            entity.ExchangeTime = DateTime.Now;
            await _orderDetailRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 判断订单完成度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task<bool> CheckedOrderStatus(Guid? id)
        {
            var list = await _orderDetailRepository.GetAll().Where(v => v.OrderId == id).ToListAsync();
            foreach (var item in list)
            {
                if (item.Status != DZEnums.DZEnums.ExchangeStatus.已兑换)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task UpdateOrderStatus(Guid id)
        {
            var entity = await _orderRepository.GetAsync(id);
            entity.Status = DZEnums.DZEnums.OrderStatus.已完成;
            entity.CompleteTime = DateTime.Now;
            await _orderRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 获取订单id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task<Guid> GetOrderId(Guid? id)
        {
            Guid orderId = await _orderDetailRepository.GetAll().Where(v => v.Id == id).Select(v => v.OrderId).FirstOrDefaultAsync();
            return orderId;
        }
    }
}

