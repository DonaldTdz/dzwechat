
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


using HC.DZWechat.WechatMessages.Dtos;
using HC.DZWechat.WechatMessages;

namespace HC.DZWechat.WechatMessages
{
    /// <summary>
    /// WechatMessage应用层服务的接口方法
    ///</summary>
    public interface IWechatMessageAppService : IApplicationService
    {
        /// <summary>
		/// 获取WechatMessage的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<WechatMessageListDto>> GetPaged(GetWechatMessagesInput input);


		/// <summary>
		/// 通过指定id获取WechatMessageListDto信息
		/// </summary>
		Task<WechatMessageListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetWechatMessageForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改WechatMessage的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateWechatMessageInput input);


        /// <summary>
        /// 删除WechatMessage信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除WechatMessage
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出WechatMessage为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();


        /// <summary>
        /// 添加或者修改WechatMessage的公共方法
        /// </summary>
        /// <returns></returns>   
        Task CreateOrUpdateDto(WechatMessageEditDto input);

    }
}
