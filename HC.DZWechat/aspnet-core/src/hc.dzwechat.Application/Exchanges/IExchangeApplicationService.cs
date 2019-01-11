
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


using HC.DZWechat.Exchanges.Dtos;
using HC.DZWechat.Exchanges;
using HC.DZWechat.CommonDto;
using HC.DZWechat.Orders.Dtos;

namespace HC.DZWechat.Exchanges
{
    /// <summary>
    /// Exchange应用层服务的接口方法
    ///</summary>
    public interface IExchangeAppService : IApplicationService
    {
        /// <summary>
		/// 获取Exchange的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExchangeListDto>> GetPaged(GetExchangesInput input);


		/// <summary>
		/// 通过指定id获取ExchangeListDto信息
		/// </summary>
		Task<ExchangeListDto> GetById(Guid id);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetExchangeForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Exchange的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ExchangeListDto> CreateOrUpdate(ExchangeEditDto input);


        /// <summary>
        /// 删除Exchange信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Exchange
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出Exchange为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

        /// <summary>
        /// 获取兑换明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExchangeListDto>> GetExchangeDetail(ExchangeInput input);

        Task<APIResultDto> ExchangeGoods(OrderEditDto input);
    }
}
