
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


using HC.DZWechat.VipPurchases.Dtos;
using HC.DZWechat.VipPurchases;

namespace HC.DZWechat.VipPurchases
{
    /// <summary>
    /// VipPurchase应用层服务的接口方法
    ///</summary>
    public interface IVipPurchaseAppService : IApplicationService
    {
        /// <summary>
		/// 获取VipPurchase的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<VipPurchaseListDto>> GetPaged(GetVipPurchasesInput input);


		/// <summary>
		/// 通过指定id获取VipPurchaseListDto信息
		/// </summary>
		Task<VipPurchaseListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetVipPurchaseForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改VipPurchase的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateVipPurchaseInput input);


        /// <summary>
        /// 删除VipPurchase信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除VipPurchase
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出VipPurchase为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
