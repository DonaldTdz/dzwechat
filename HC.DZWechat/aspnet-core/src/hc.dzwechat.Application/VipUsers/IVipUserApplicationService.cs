
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


using HC.DZWechat.VipUsers.Dtos;
using HC.DZWechat.VipUsers;

namespace HC.DZWechat.VipUsers
{
    /// <summary>
    /// VipUser应用层服务的接口方法
    ///</summary>
    public interface IVipUserAppService : IApplicationService
    {
        /// <summary>
		/// 获取VipUser的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<VipUserListDto>> GetPaged(GetVipUsersInput input);


		/// <summary>
		/// 通过指定id获取VipUserListDto信息
		/// </summary>
		Task<VipUserListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetVipUserForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改VipUser的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateVipUserInput input);


        /// <summary>
        /// 删除VipUser信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除VipUser
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<bool> BindVipUser(GetWXVipUserInput input);
        Task<WXVipUserListDto> GetVipUserById(GetWXVipUserInput input);
    }
}
