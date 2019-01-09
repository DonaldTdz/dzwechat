
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


using HC.DZWechat.Categorys.Dtos;
using HC.DZWechat.Categorys;

namespace HC.DZWechat.Categorys
{
    /// <summary>
    /// Category应用层服务的接口方法
    ///</summary>
    public interface ICategoryAppService : IApplicationService
    {
        /// <summary>
		/// 获取Category的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CategoryListDto>> GetPaged(GetCategorysInput input);


        /// <summary>
        /// 通过指定id获取CategoryListDto信息
        /// </summary>
        Task<CategoryListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCategoryForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改Category的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CategoryListDto> CreateOrUpdate(CategoryEditDto input);


        /// <summary>
        /// 删除Category信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除Category
        /// </summary>
        Task BatchDelete(List<int> input);

        Task<List<SelectGroup>> GetCategorySelectGroup();

        Task<List<GoodsCategoryDto>> GetGoodsCategoriesAsync();
    }
}
