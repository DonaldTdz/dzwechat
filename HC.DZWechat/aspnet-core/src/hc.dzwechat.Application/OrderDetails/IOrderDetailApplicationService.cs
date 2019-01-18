
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


using HC.DZWechat.OrderDetails.Dtos;
using HC.DZWechat.OrderDetails;

namespace HC.DZWechat.OrderDetails
{
    /// <summary>
    /// OrderDetail应用层服务的接口方法
    ///</summary>
    public interface IOrderDetailAppService : IApplicationService
    {
        /// <summary>
		/// 获取OrderDetail的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrderDetailListDto>> GetPaged(GetOrderDetailsInput input);


		/// <summary>
		/// 通过指定id获取OrderDetailListDto信息
		/// </summary>
		Task<OrderDetailListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetOrderDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改OrderDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateOrderDetailInput input);


        /// <summary>
        /// 删除OrderDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除OrderDetail
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<List<WXOrderDetailListDto>> GetOrderDetailListByIdAsync(GetOrderDetailsInput input);
    }
}
