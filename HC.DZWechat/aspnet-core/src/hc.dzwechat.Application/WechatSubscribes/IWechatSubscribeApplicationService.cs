
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


using HC.DZWechat.WechatSubscribes.Dtos;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.WechatSubscribes
{
    /// <summary>
    /// WechatSubscribe应用层服务的接口方法
    ///</summary>
    public interface IWechatSubscribeAppService : IApplicationService
    {
        /// <summary>
		/// 获取WechatSubscribe的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<WechatSubscribeListDto>> GetPaged(GetWechatSubscribesInput input);


		/// <summary>
		/// 通过指定id获取WechatSubscribeListDto信息
		/// </summary>
		Task<WechatSubscribeListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetWechatSubscribeForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改WechatSubscribe的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateWechatSubscribeInput input);


        /// <summary>
        /// 删除WechatSubscribe信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除WechatSubscribe
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出WechatSubscribe为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
