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
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Template;
using Senparc.Weixin;
using HC.DZWechat.Shops;
using HC.DZWechat.WechatConfigs;

namespace HC.DZWechat.ScanExchange
{
    public class ScanExchangeManager : DZWechatDomainServiceBase, IScanExchangeManager
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IRepository<WechatUser, Guid> _wechatuserRepository;
        private readonly IRepository<OrderDetail, Guid> _orderDetailRepository;
        private readonly IRepository<Exchange, Guid> _exchangeRepository;
        private readonly IRepository<Shop, Guid> _shopRepository;
        private readonly IRepository<WechatConfig, Guid> _configRepository;

        public ScanExchangeManager(IRepository<Order, Guid> repository
            , IRepository<OrderDetail, Guid> orderDetailRepository
            , IRepository<WechatUser, Guid> wechatuserRepository
            , IRepository<Exchange, Guid> exchangeRepository
            , IRepository<Shop, Guid> shopRepository
            , IRepository<WechatConfig, Guid> configRepository
        )
        {
            _repository = repository;
            _orderDetailRepository = orderDetailRepository;
            _wechatuserRepository = wechatuserRepository;
            _exchangeRepository = exchangeRepository;
            _shopRepository = shopRepository;
            _configRepository = configRepository;
        }


        /// <summary>
        /// 判断用户是否关注
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool GetIsAttentionByIdAsync(string openId)
        {
            int weChat = _wechatuserRepository.GetAll().Where(v => v.OpenId == openId && v.BindStatus == DZEnums.DZCommonEnums.BindStatus.取消关注).Count();
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
        public async Task<APIResultDto> ExchangeGoods(Guid orderId, string openId, Guid shopId)
        {
            Guid userId = await _wechatuserRepository.GetAll().Where(v => v.OpenId == openId).Select(v => v.Id).FirstOrDefaultAsync();
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
            //发送模板消息
            var orderInfo = await _repository.GetAsync(orderId);
            string wxOpenId = await _wechatuserRepository.GetAll().Where(v => v.Id == orderInfo.UserId).Select(v => v.WxOpenId).FirstOrDefaultAsync();
            string shopName = await _shopRepository.GetAll().Where(v => v.Id == shopId).Select(v => v.Name).FirstOrDefaultAsync();
            await OrderInfoMesssage(wxOpenId, orderInfo.Number, shopName);
            return new APIResultDto() { Code = 0, Msg = "兑换成功" };
        }

        /// <summary>
        /// 判断订单完成度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckedOrderStatus(Guid orderId)
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
        protected virtual async Task UpdateExchangeAsync(Guid orderDetailId, Guid shopId, Guid userId)
        {
            var entity = new Exchange();
            entity.CreationTime = DateTime.Now;
            entity.ExchangeCode = DZEnums.DZCommonEnums.ExchangeCodeEnum.线下兑换;
            entity.ShopId = shopId;
            entity.WechatUserId = userId;
            entity.OrderDetailId = orderDetailId;
            await _exchangeRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 兑换成功通知
        /// </summary>
        /// <param name="wxOpenId"></param>
        /// <param name="orderNo"></param>
        /// <param name="shopName"></param>
        /// <returns></returns>
        private async Task OrderInfoMesssage(string wxOpenId, string orderNo, string shopName)
        {
            try
            {
                string appId = Config.SenparcWeixinSetting.WxOpenAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
                string templateId = await _configRepository.GetAll().Select(v => v.TemplateIds).FirstOrDefaultAsync();
                //string templateId = "hO16T1Sh_0R_7ttTqkCbpECOXfPEIOk3f9T6Of_ohYQ";
                if (!string.IsNullOrEmpty(templateId))
                {
                    string[] ids = templateId.Split(',');
                    object data = new
                    {
                        keyword1 = new TemplateDataItem(orderNo),//订单编号
                        keyword2 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),//兑换时间
                        keyword3 = new TemplateDataItem(shopName),//商家名称
                        keyword4 = new TemplateDataItem("恭喜您兑换成功，请核实商品信息")//备注信息
                    };
                    await TemplateApi.SendTemplateMessageAsync(appId, wxOpenId, ids[1], data, "formSubmit");
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("兑换成功发送消息通知失败 error：{0} Exception：{1}", ex.Message, ex);
            }
        }
    }
}
