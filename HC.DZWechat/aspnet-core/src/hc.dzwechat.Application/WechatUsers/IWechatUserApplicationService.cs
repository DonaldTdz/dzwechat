
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


using HC.DZWechat.WechatUsers.Dtos;
using HC.DZWechat.WechatUsers;

namespace HC.DZWechat.WechatUsers
{
    /// <summary>
    /// WechatUser应用层服务的接口方法
    ///</summary>
    public interface IWechatUserAppService : IApplicationService
    {
        /// <summary>
		/// 获取WechatUser的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<WechatUserListDto>> GetPaged(GetWechatUsersInput input);


        /// <summary>
        /// 通过指定id获取WechatUserListDto信息
        /// </summary>
        Task<WechatUserListDto> GetById(Guid id);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetWechatUserForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改WechatUser的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateWechatUserInput input);


        /// <summary>
        /// 删除WechatUser信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除WechatUser
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出WechatUser为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
