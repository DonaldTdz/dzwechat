
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


using HC.DZWechat.Deliverys.Dtos;
using HC.DZWechat.Deliverys;

namespace HC.DZWechat.Deliverys
{
    /// <summary>
    /// Delivery应用层服务的接口方法
    ///</summary>
    public interface IDeliveryAppService : IApplicationService
    {
        /// <summary>
		/// 获取Delivery的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DeliveryListDto>> GetNoPaged(GetDeliverysInput input);


		/// <summary>
		/// 通过指定id获取DeliveryListDto信息
		/// </summary>
		Task<DeliveryListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDeliveryForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Delivery的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task CreateOrUpdate(CreateOrUpdateDeliveryInput input);


        /// <summary>
        /// 删除Delivery信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task WXDelete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Delivery
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<DeliveryListDto> WXCreateOrUpdate(DeliveryWXEditDto input);
        Task<List<DeliveryListDto>> GetWxDeliveryListAsync(EntityDto<string> input);
        Task<DeliveryListDto> GetWxDeliveryByIdAsync(EntityDto<Guid> input);
    }
}
