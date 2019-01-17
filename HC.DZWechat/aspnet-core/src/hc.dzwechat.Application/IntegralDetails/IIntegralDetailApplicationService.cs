
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


using HC.DZWechat.IntegralDetails.Dtos;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.Dtos;

namespace HC.DZWechat.IntegralDetails
{
    /// <summary>
    /// IntegralDetail应用层服务的接口方法
    ///</summary>
    public interface IIntegralDetailAppService : IApplicationService
    {
        /// <summary>
		/// 获取IntegralDetail的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<IntegralDetailListDto>> GetPaged(GetIntegralDetailsInput input);


		/// <summary>
		/// 通过指定id获取IntegralDetailListDto信息
		/// </summary>
		Task<IntegralDetailListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetIntegralDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改IntegralDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateIntegralDetailInput input);


        /// <summary>
        /// 删除IntegralDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除IntegralDetail
        /// </summary>
        Task BatchDelete(List<Guid> input);


        Task<PagedResultDto<IntegralDetailListDto>> GetPagedById(GetIntegralDetailsInput input);

        /// <summary>
        /// 获取积分变化统计（按月）
        /// </summary>
        /// <returns></returns> 
        Task<List<IntegralDetailDto>> GetIntegralDetailByMonthAsync(int searchMonth);
        Task SignInAsync(EntityDto<string> input);
        Task<SignInDto> GetUserTodayIsSignIn(string WxOpenId);
        Task<WxPagedResultDto<WXIntegralDetailListDto>> GetIntegralDetailListAsync(GetWxIntegralDetailsInput input);
        Task<WxPagedResultDto<WXIntegralDetailListDto>> GetIntegralAllListAsync(GetWxIntegralDetailsInput input);
        Task<decimal?> GetUserIntegralTotalAsync(EntityDto<string> input);
    }
}
