
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
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.Shops;
using HC.DZWechat.ScanExchange;
using HC.DZWechat.CommonDto;
using HC.DZWechat.Orders.Dtos;
using HC.DZWechat.ScanExchange.Dtos;
using HC.DZWechat.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Template;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin;
using HC.DZWechat.WechatUsers;
using HC.DZWechat.WechatConfigs;

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
        private readonly IRepository<Shop, Guid> _shopRepository;
        private readonly IRepository<WechatUser, Guid> _wechatUserRepository;
        private readonly IRepository<WechatConfig, Guid> _configRepository;

        private readonly IExchangeManager _entityManager;
        private readonly IScanExchangeManager _scanExchangeManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ExchangeAppService(
        IRepository<Exchange, Guid> entityRepository
        , IRepository<OrderDetail, Guid> orderDetailRepository
        , IRepository<Order, Guid> orderRepository, IRepository<Shop, Guid> shopRepository
        , IRepository<WechatUser, Guid> wechatUserRepository
        , IExchangeManager entityManager, IScanExchangeManager scanExchangeManager
        , IHostingEnvironment hostingEnvironment
        , IRepository<WechatConfig, Guid> configRepository

        )
        {
            _entityRepository = entityRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _entityManager = entityManager;
            _shopRepository = shopRepository;
            _scanExchangeManager = scanExchangeManager;
            _hostingEnvironment = hostingEnvironment;
            _wechatUserRepository = wechatUserRepository;
            _configRepository = configRepository;
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
            //发送模板消息
            var orderInfo = await GetOrderInfoAsync(orderId);
            string wxOpenId =await _wechatUserRepository.GetAll().Where(v => v.Id == orderInfo.UserId).Select(v => v.WxOpenId).FirstOrDefaultAsync();
            await LogisticsInfoMesssage(wxOpenId, orderInfo.Number, input.LogisticsCompany, input.LogisticsNo);
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
            entity.Status = ExchangeStatus.已兑换;
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
                if (item.Status != ExchangeStatus.已兑换)
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
            entity.Status = OrderStatus.已完成;
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

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task<OrderListDto> GetOrderInfoAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            return order.MapTo<OrderListDto>();
        }

        /// <summary>
        /// 获取兑换明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ExchangeListDto>> GetExchangeDetail(ExchangeInput input)
        {
            var queryE = _entityRepository.GetAll().WhereIf(input.ShopId.HasValue, e => e.ShopId == input.ShopId)
                                                  .WhereIf(input.ExchangeStyle.HasValue, e => e.ExchangeCode == input.ExchangeStyle)
                                                  .WhereIf(input.StartTime.HasValue, e => e.CreationTime >= input.StartTime)
                                                  .WhereIf(input.EndTime.HasValue, e => e.CreationTime <= input.EndTimeAddOne);

            var queryOD = _orderDetailRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.GoodsName), o => o.Specification.Contains(input.GoodsName));
            var queryO = _orderRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.OrderId), o => o.Number.Contains(input.OrderId));

            var query = from e in queryE
                        join od in queryOD on e.OrderDetailId equals od.Id
                        join o in queryO on od.OrderId equals o.Id
                        join s in _shopRepository.GetAll() on e.ShopId equals s.Id into se
                        from ses in se.DefaultIfEmpty()
                        select new ExchangeListDto
                        {
                            Id = e.Id,
                            OrderDetailId = e.OrderDetailId,
                            ExchangeCode = e.ExchangeCode,
                            ShopId = e.ShopId,
                            CreationTime = e.CreationTime,
                            LogisticsCompany = e.LogisticsCompany,
                            LogisticsNo = e.LogisticsNo,
                            ShopName = ses.Name,
                            OrderNumber = o.Number,
                            Specification = od.Specification,
                            OrderId = od.OrderId
                        };

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderByDescending(e => e.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ExchangeListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ExchangeListDto>>();
            return new PagedResultDto<ExchangeListDto>(count, entityListDtos);
        }
        /// 兑换商品
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> ExchangeGoods(ExchangeDtoInput input)
        {
            var result = await _scanExchangeManager.ExchangeGoods(input.OrderId, input.OpenId, input.ShopId);
            return result;
        }
        [AbpAllowAnonymous]
        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var result = await _scanExchangeManager.GetOrderByIdAsync(orderId);
            return result;
        }
        #region 导出兑换明细

        /// <summary>
        /// 获取兑换明细的导出数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ExchangeListDto>> GetExchangeDetailForExcel(ExchangeInput input)
        {
            var queryE = _entityRepository.GetAll().WhereIf(input.ShopId.HasValue, e => e.ShopId == input.ShopId)
                                                 .WhereIf(input.ExchangeStyle.HasValue, e => e.ExchangeCode == input.ExchangeStyle)
                                                 .WhereIf(input.StartTime.HasValue, e => e.CreationTime >= input.StartTime)
                                                 .WhereIf(input.EndTime.HasValue, e => e.CreationTime <= input.EndTimeAddOne);
            var queryOD = _orderDetailRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.GoodsName), o => o.Specification.Contains(input.GoodsName));
            var queryO = _orderRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.OrderId), o => o.Number.Contains(input.OrderId));
            var query = from e in queryE
                        join od in queryOD on e.OrderDetailId equals od.Id
                        join o in queryO on od.OrderId equals o.Id
                        join s in _shopRepository.GetAll() on e.ShopId equals s.Id into se
                        from ses in se.DefaultIfEmpty()
                        select new ExchangeListDto
                        {
                            Id = e.Id,
                            OrderDetailId = e.OrderDetailId,
                            ExchangeCode = e.ExchangeCode,
                            ShopId = e.ShopId,
                            CreationTime = e.CreationTime,
                            LogisticsCompany = e.LogisticsCompany,
                            LogisticsNo = e.LogisticsNo,
                            ShopName = ses.Name,
                            OrderNumber = o.Number,
                            Specification = od.Specification,
                            OrderId = od.OrderId
                        };
            var entityList = await query
                    .OrderByDescending(e => e.CreationTime).AsNoTracking()
                    .ToListAsync();

            return entityList;
        }
        /// <summary>
        /// 创建兑换明细表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        public string CreateExchangeDetailExcel(string fileName, List<ExchangeListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("SheduleSum");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "订单编号", "店铺名称", "商品名称", "兑换方式", "兑换时间", "物流单号", "物流公司" };
                var fontTitle = workbook.CreateFont();
                fontTitle.IsBold = true;
                for (int i = 0; i < titles.Length; i++)
                {
                    var cell = titleRow.CreateCell(i);
                    cell.CellStyle.SetFont(fontTitle);
                    cell.SetCellValue(titles[i]);
                    //ExcelHelper.SetCell(titleRow.CreateCell(i), fontTitle, titles[i]);
                }
                var font = workbook.CreateFont();
                foreach (var item in data)
                {

                    rowIndex++;
                    IRow row = sheet.CreateRow(rowIndex);
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.OrderNumber);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.ShopName);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.Specification);
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.ExchangeCodeName);
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.LogisticsNo);
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.LogisticsCompany);
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
        /// <summary>
        /// 导出兑换明细表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportExchangeDetail(ExchangeInput input)
        {
            try
            {
                var exportData = await GetExchangeDetailForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateExchangeDetailExcel("兑换明细.xlsx", exportData);
                return result;

            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportExchangeDetail errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }
        #endregion


        /// <summary>
        /// 店铺兑换记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ExchangeListDto>> GetPagedByShopId(GetExchangesInput input)
        {
            var exchange = _entityRepository.GetAll().Where(v => v.ShopId == input.ShopId).WhereIf(input.StartTime.HasValue, v => v.CreationTime >= input.StartTime && v.CreationTime <= input.EndTime);
            var orderDetails = _orderDetailRepository.GetAll();
            var order = _orderRepository.GetAll();

            var result = (from e in exchange
                          join od in orderDetails on e.OrderDetailId equals od.Id
                          join o in order on od.OrderId equals o.Id
                          select new ExchangeListDto()
                          {
                              Id = e.Id,
                              OrderId = o.Id,
                              Specification = od.Specification,
                              OrderNumber = o.Number,
                              DeliveryName = o.DeliveryName,
                              DeliveryPhone = o.Phone,
                              CreationTime = e.CreationTime,
                              LogisticsCompany = e.LogisticsCompany,
                              LogisticsNo = e.LogisticsNo,
                              ExchangeCode = e.ExchangeCode
                          }).WhereIf(!string.IsNullOrEmpty(input.FilterText), v => v.Specification.Contains(input.FilterText) || v.DeliveryName.Contains(input.FilterText) || v.DeliveryPhone.Contains(input.FilterText) || v.OrderNumber.Contains(input.FilterText));

            var count = await result.CountAsync();
            var entityList = await result
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            return new PagedResultDto<ExchangeListDto>(count, entityList);
        }

        /// <summary>
        /// 汇总下拉列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectGroups>> GetShopTypeAsync()
        {
            var entity = await (from c in _shopRepository.GetAll()
                                select new
                                {
                                    text = c.Name,
                                    value = c.Id,
                                }).OrderBy(v => v.text).AsNoTracking().ToListAsync();
            var YingXiao = new SelectGroups();
            YingXiao.text = "营销中心";
            YingXiao.value = "9999";
            var result = entity.MapTo<List<SelectGroups>>();
            result.Add(YingXiao);
            return result;
        }

        /// <summary>
        /// 兑换统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ExchangeSummary> GetExchangeSummary(GetExchangesInput input)
        {
            var exchange = _entityRepository.GetAll().WhereIf(input.StartTime.HasValue, v => v.CreationTime >= input.StartTime && v.CreationTime <= input.EndTime).AsNoTracking();
            var orderDetail = _orderDetailRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.FilterText), v => v.Specification.Contains(input.FilterText)).AsNoTracking();
            var shop = _shopRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.ShopType) && input.ShopType != "9999", v => v.Id == new Guid(input.ShopType)).AsNoTracking();

            var resultC = new List<ExchangeSummary>();
            var resultS = new List<ExchangeSummary>();
            if (input.ShopType == "9999" || string.IsNullOrEmpty(input.ShopType))
            {
                //营销中心兑换记录
                resultC = (from e in exchange
                           where e.ShopId.HasValue == false
                           join od in orderDetail on e.OrderDetailId equals od.Id
                           group new { od.Specification, od.Num } by new { od.Specification } into g
                           select new ExchangeSummary()
                           {
                               ShopName = "营销中心",
                               Specification = g.Key.Specification,
                               ExchangeNum = (int)g.Sum(v => v.Num)
                           }).AsNoTracking().ToList();
            }
            if (input.ShopType != "9999" || string.IsNullOrEmpty(input.ShopType))
            {
                //直营店兑换记录
                resultS = (from e in exchange
                           join s in shop on e.ShopId equals s.Id
                           join od in orderDetail on e.OrderDetailId equals od.Id
                           group new { od.Specification, s.Name, od.Num } by new { od.Specification, s.Name } into g
                           select new ExchangeSummary()
                           {
                               ShopName = g.Key.Name,
                               Specification = g.Key.Specification,
                               ExchangeNum = (int)g.Sum(v => v.Num)
                           }).AsNoTracking().OrderBy(v => v.ShopName).ToList();
            }
            var list = new List<ExchangeSummary>();
            if (resultS.Count > 0)
            {
                list.AddRange(resultS);
            }
            if (resultC.Count > 0)
            {
                list.AddRange(resultC);
            }
            var sum = new ExchangeSummary();
            sum.ShopName = "汇总";
            sum.Specification = "/";
            sum.ExchangeNum = list.Sum(v => v.ExchangeNum ?? 0);
            list.Add(sum);
            //var count = list.Count();
            return list;
        }

        /// <summary>
        /// 兑换明细汇总
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportExchangeSummary(GetExchangesInput input)
        {
            try
            {
                var exportData = GetExchangeSummary(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateExchangeSummaryExcel("兑换汇总.xlsx", exportData);
                return result;

            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportExchangeSummary errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        /// <summary>
        /// 创建兑换汇总
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        public string CreateExchangeSummaryExcel(string fileName, List<ExchangeSummary> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("ExchangeSum");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "店铺名称", "商品名称", "兑换数量" };
                var fontTitle = workbook.CreateFont();
                fontTitle.IsBold = true;
                for (int i = 0; i < titles.Length; i++)
                {
                    var cell = titleRow.CreateCell(i);
                    cell.CellStyle.SetFont(fontTitle);
                    cell.SetCellValue(titles[i]);
                    //ExcelHelper.SetCell(titleRow.CreateCell(i), fontTitle, titles[i]);
                }
                var font = workbook.CreateFont();
                foreach (var item in data)
                {

                    rowIndex++;
                    IRow row = sheet.CreateRow(rowIndex);
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.ShopName);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.Specification);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.ExchangeNum.ToString());
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }

        /// <summary>
        /// 发送物流通知
        /// </summary>
        /// <param name="wxOpenId"></param>
        /// <param name="orderNo"></param>
        /// <param name="logisticsCompany"></param>
        /// <param name="logisticsNo"></param>
        /// <returns></returns>
        private async Task LogisticsInfoMesssage(string wxOpenId, string orderNo, string logisticsCompany, string logisticsNo)
        {
            try
            {
                string appId = Config.SenparcWeixinSetting.WxOpenAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
                string templateId = await _configRepository.GetAll().Select(v=>v.TemplateIds).FirstOrDefaultAsync();
                //string templateId = "i8ME9xCmEXv1qqq3uNiCtEjuiYEVk4QYAi7zZuSt6t0";
                if (!string.IsNullOrEmpty(templateId))
                {
                    string[] ids = templateId.Split(',');
                    object data = new
                    {
                        keyword1 = new TemplateDataItem("尊敬的用户您好，您的订单已发货"),//温馨提示
                        keyword2 = new TemplateDataItem(orderNo),//订单编号
                        keyword3 = new TemplateDataItem(logisticsNo),//物流单号
                        keyword4 = new TemplateDataItem(logisticsCompany)//物流公司
                    };
                    await TemplateApi.SendTemplateMessageAsync(appId, wxOpenId, ids[0], data, "formSubmit");
                }
            }
            catch (Exception ex)
            {

                Logger.ErrorFormat("订单物流通知发送消息通知失败 error：{0} Exception：{1}", ex.Message, ex);
            }
        }
    }
}


