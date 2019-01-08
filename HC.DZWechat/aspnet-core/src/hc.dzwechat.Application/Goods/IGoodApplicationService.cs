
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


using HC.DZWechat.Goods.Dtos;
using HC.DZWechat.Goods;
using HC.DZWechat.Dtos;

namespace HC.DZWechat.Goods
{
    /// <summary>
    /// Good应用层服务的接口方法
    ///</summary>
    public interface IGoodAppService : IApplicationService
    {
        /// <summary>
		/// 获取Good的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GoodListDto>> GetPaged(GetGoodsInput input);


		/// <summary>
		/// 通过指定id获取GoodListDto信息
		/// </summary>
		Task<GoodListDto> GetById(Guid id);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetGoodForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Good的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GoodListDto> CreateOrUpdate(GoodEditDto input);


        /// <summary>
        /// 删除Good信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Good
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出Good为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

        
        Task<WxPagedResultDto<GoodsGridDto>> GetHeatGoodsAsync(WxPagedInputDto input);

        Task<WxPagedResultDto<GoodsGridDto>> GetSearchGoodsAsync(GoodsSearchInputDto input);
    }
}
