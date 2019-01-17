
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


using HC.DZWechat.Orders;
using HC.DZWechat.Orders.Dtos;
using HC.DZWechat.Orders.DomainService;
using HC.DZWechat.WechatUsers;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.OrderDetails;

namespace HC.DZWechat.Orders
{
    /// <summary>
    /// Order应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class OrderAppService : DZWechatAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _entityRepository;
        private readonly IRepository<WechatUser, Guid> _wechatUserRepository;
        private readonly IRepository<IntegralDetail, Guid> _integralRepository;
        private readonly IRepository<OrderDetail, Guid> _orderdetailRepository;

        private readonly IOrderManager _entityManager;


        /// <summary>
        /// 构造函数 
        ///</summary>
        public OrderAppService(
        IRepository<Order, Guid> entityRepository
        , IOrderManager entityManager, IRepository<WechatUser, Guid> wechatUserRepository
        , IRepository<IntegralDetail, Guid> integralRepository, IRepository<OrderDetail, Guid> orderdetailRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _wechatUserRepository = wechatUserRepository;
            _integralRepository = integralRepository;
            _orderdetailRepository = orderdetailRepository;
        }


        /// <summary>
        /// 获取Order的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<OrderListDto>> GetPaged(GetOrdersInput input)
        {

            var query = _entityRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.FilterText),
            u => u.Number.Contains(input.FilterText)
            || u.DeliveryName.Contains(input.FilterText)
            || u.DeliveryPhone.Contains(input.FilterText))
                .WhereIf(input.Status.HasValue, v => v.Status == input.Status.Value)
                .WhereIf(input.IsUnMailing, u => _orderdetailRepository.GetAll().Any(d => d.OrderId == u.Id && d.Status == ExchangeStatus.未兑换 && d.ExchangeCode == ExchangeCodeEnum.邮寄兑换));

            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<OrderListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<OrderListDto>>();

            return new PagedResultDto<OrderListDto>(count, entityListDtos);
        }

        public async Task<PagedResultDto<OrderListDto>> GetPagedById(GetOrdersInput input)
        {

            var query = _entityRepository.GetAll().Where(v => v.UserId == input.Id);
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<OrderListDto>>();

            return new PagedResultDto<OrderListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 通过指定id获取OrderListDto信息
        /// </summary>

        public async Task<OrderListDto> GetById(Guid id)
        {
            var entity = await _entityRepository.GetAsync(id);
            return entity.MapTo<OrderListDto>();
        }

        /// <summary>
        /// 获取编辑 Order
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetOrderForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetOrderForEditOutput();
            OrderEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<OrderEditDto>();

                //orderEditDto = ObjectMapper.Map<List<orderEditDto>>(entity);
            }
            else
            {
                editDto = new OrderEditDto();
            }

            output.Order = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Order的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateOrderInput input)
        {

            if (input.Order.Id.HasValue)
            {
                await Update(input.Order);
            }
            else
            {
                await Create(input.Order);
            }
        }


        /// <summary>
        /// 新增Order
        /// </summary>

        protected virtual async Task<OrderEditDto> Create(OrderEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Order>(input);
            var entity = input.MapTo<Order>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<OrderEditDto>();
        }

        /// <summary>
        /// 编辑Order
        /// </summary>

        protected virtual async Task Update(OrderEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Order信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Order的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出Order为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        /// <summary>
        /// 主页数据统计
        /// </summary>
        /// <returns></returns>
        public async Task<HomeInfo> GetHomeInfo()
        {
            var homeInfo = new HomeInfo();
            homeInfo.WeChatUsersTotal = await _wechatUserRepository.GetAll().CountAsync();
            homeInfo.IntegralTotal = (int)await _wechatUserRepository.GetAll().SumAsync(i => i.Integral);
            homeInfo.OrderTotal = await _entityRepository.GetAll().Where(o => o.Status != OrderStatus.已取消).CountAsync();
            homeInfo.PendingOrderTotal = await _entityRepository.GetAll().Where(o => o.Status == OrderStatus.已支付).CountAsync();
            return homeInfo;
        }

        /// <summary>
        /// 获取最新支付待处理的前6条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ProcesseingOrderListDto> GetOrderTopSix()
        {
            var result = new ProcesseingOrderListDto();
            var query = from o in _entityRepository.GetAll().Where(o => o.Status != OrderStatus.已支付 && o.Status != OrderStatus.已取消)
                        join od in _orderdetailRepository.GetAll().Where(od => od.ExchangeCode == ExchangeCodeEnum.邮寄兑换 && od.Status == ExchangeStatus.未兑换) on o.Id equals od.OrderId
                        select new OrderListDto
                        {
                            Id = o.Id,
                            Number = o.Number,
                            NickName = o.NickName,
                            UserId = o.UserId,
                            Phone = o.Phone,
                            Status = o.Status,
                            PayTime = o.PayTime,
                        };
            var list = await query.OrderByDescending(o => o.PayTime).Take(6).ToListAsync();
            result.Orders = list.MapTo<List<OrderListDto>>();
            result.Count = await query.CountAsync();
            return result;
        }
    }
}


