using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using HC.DZWechat.Orders;
using HC.DZWechat.WechatUsers;
using HC.DZWechat.OrderDetails;
using HC.DZWechat.ScanExchange.Dtos;
using HC.DZWechat.CommonDto;
using HC.DZWechat.Exchanges;

namespace HC.DZWechat.ScanExchange
{
    public class ScanExchangeManager : DZWechatDomainServiceBase, IScanExchangeManager
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IRepository<WechatUser, Guid> _wechatuserRepository;
        private readonly IRepository<OrderDetail, Guid> _orderDetailRepository;
        private readonly IRepository<Exchange, Guid> _exchangeRepository;

        public ScanExchangeManager(IRepository<Order, Guid> repository
            , IRepository<OrderDetail, Guid> orderDetailRepository
            , IRepository<WechatUser, Guid> wechatuserRepository
            , IRepository<Exchange, Guid> exchangeRepository
        )
        {
            _repository = repository;
            _orderDetailRepository = orderDetailRepository;
            _wechatuserRepository = wechatuserRepository;
            _exchangeRepository = exchangeRepository;
        }


        /// <summary>
        /// 判断用户是否关注
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool GetIsAttentionByIdAsync(string openId)
        {
            int weChat =  _wechatuserRepository.GetAll().Where(v => v.OpenId == openId && v.BindStatus == DZEnums.DZCommonEnums.BindStatus.取消关注).Count();
            if (weChat == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否为店铺管理员
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<bool> IsShopManagerByIdAsync(string openId)
        {
            int count = await _wechatuserRepository.GetAll().Where(v => v.OpenId == openId && v.IsShopManager == true).CountAsync();
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取兑换商品信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<List<OrderDetailDto>> GetExchangeGoodsByIdAsync(Guid orderId, string openId)
        {
            var exchangeList = await _orderDetailRepository.GetAll().Where(v => v.OrderId == orderId && v.Status == DZEnums.DZCommonEnums.ExchangeStatus.未兑换).Select
                (v => new OrderDetailDto()
                {
                    Id = v.Id,
                    Specification = v.Specification,
                    Unit = v.Unit,
                    Status = v.Status,
                    Num = v.Num,
                    Integral = v.Integral
                }).ToListAsync();
            return exchangeList;
        }

        /// <summary>
        /// 兑换商品
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExchangeGoods(Guid orderId,string openId,Guid shopId)
        {
            Guid userId =await _wechatuserRepository.GetAll().Where(v => v.OpenId == openId).Select(v => v.Id).FirstOrDefaultAsync();
            var orderDetails = await _orderDetailRepository.GetAll().Where(v => v.OrderId == orderId && v.Status == DZEnums.DZCommonEnums.ExchangeStatus.未兑换).ToListAsync();
            foreach (var item in orderDetails)
            {
                item.Status = DZEnums.DZCommonEnums.ExchangeStatus.已兑换;
                item.ExchangeTime = DateTime.Now;
                await UpdateExchangeAsync(item.Id, shopId, userId);
            }
            bool isOk = await CheckedOrderStatus(orderId);
            if (isOk == true)
            {
                await UpdateOrderStatus(orderId);
            }
            return new APIResultDto() { Code = 0, Msg = "兑换成功" };
        }

        /// <summary>
        /// 判断订单完成度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task<bool> CheckedOrderStatus(Guid orderId)
        {
            var list = await _orderDetailRepository.GetAll().Where(v => v.OrderId == orderId).ToListAsync();
            foreach (var item in list)
            {
                if (item.Status != DZEnums.DZCommonEnums.ExchangeStatus.已兑换)
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
        protected virtual async Task UpdateOrderStatus(Guid orderId)
        {
            var entity = await _repository.GetAsync(orderId);
            entity.Status = DZEnums.DZCommonEnums.OrderStatus.已完成;
            entity.CompleteTime = DateTime.Now;
            await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var result = await _repository.GetAll().Where(v => v.Id == orderId).Select
                (v => new OrderDto()
                {
                    Id = v.Id,
                    Number = v.Number,
                    Status = v.Status
                }).FirstOrDefaultAsync();
            return result;
        }


        /// <summary>
        /// 生成线下兑换记录
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <param name="shopId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        protected virtual async Task UpdateExchangeAsync(Guid orderDetailId,Guid shopId,Guid userId)
        {
            var entity = new Exchange();
            entity.CreationTime = DateTime.Now;
            entity.ExchangeCode = DZEnums.DZCommonEnums.ExchangeCodeEnum.线下兑换;
            entity.ShopId = shopId;
            entity.WechatUserId = userId;
            entity.OrderDetailId = orderDetailId;
            await _exchangeRepository.InsertAsync(entity);
        }
    }
}
