
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


using HC.DZWechat.Newses.Dtos;
using HC.DZWechat.Newses;
using static HC.DZWechat.DZEnums.DZEnums;

namespace HC.DZWechat.Newses
{
    /// <summary>
    /// News应用层服务的接口方法
    ///</summary>
    public interface INewsAppService : IApplicationService
    {
        /// <summary>
		/// 获取News的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<NewsListDto>> GetPaged(GetNewssInput input);


		/// <summary>
		/// 通过指定id获取NewsListDto信息
		/// </summary>
		Task<NewsListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetNewsForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改News的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateNewsInput input);


        /// <summary>
        /// 删除News信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除News
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出News为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();


            
        Task<NewsListDto> GetByIdAndType(Guid? id, NewsType newsType);

    }
}
