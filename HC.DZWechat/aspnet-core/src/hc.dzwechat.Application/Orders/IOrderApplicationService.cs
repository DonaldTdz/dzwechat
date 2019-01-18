
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using HC.DZWechat.Orders.Dtos;
using HC.DZWechat.Orders;
using HC.DZWechat.Dtos;
using HC.DZWechat.CommonDto;

namespace HC.DZWechat.Orders
{
    /// <summary>
    /// Order应用层服务的接口方法
    ///</summary>
    public interface IOrderAppService : IApplicationService
    {
        /// <summary>
		/// 获取Order的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrderListDto>> GetPaged(GetOrdersInput input);


        /// <summary>
        /// 通过指定id获取OrderListDto信息
        /// </summary>
        Task<OrderListDto> GetById(Guid id);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetOrderForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Order的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateOrderInput input);


        /// <summary>
        /// 删除Order信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Order
        /// </summary>
        Task BatchDelete(List<Guid> input);


        Task<PagedResultDto<OrderListDto>> GetPagedById(GetOrdersInput input);
        /// <summary>
        /// 主页数据统计
        /// </summary>
        /// <returns></returns>
        Task<HomeInfo> GetHomeInfo();

        /// <summary>
        /// 获取最新支付待处理的前6条数据
        /// </summary>
        /// <returns></returns>
        Task<ProcesseingOrderListDto> GetOrderTopSix();
        Task<WxPagedResultDto<OrderListDto>> GetOrderListAsync(GetWxOrderInput input);
        Task<OrderListDto> GetOrderByIdAsync(GetWxOrderInput input);

        Task<CommonDto.APIResultDto> SaveOrderAsync(SaveOrderInput input);
        Task CancelOrderByIdAsync(SaveOrderInput input);
    }
}
