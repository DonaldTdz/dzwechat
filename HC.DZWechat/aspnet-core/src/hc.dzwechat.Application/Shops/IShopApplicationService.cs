
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


using HC.DZWechat.Shops.Dtos;
using HC.DZWechat.Shops;
using HC.DZWechat.ScanExchange.Dtos;

namespace HC.DZWechat.Shops
{
    /// <summary>
    /// Shop应用层服务的接口方法
    ///</summary>
    public interface IShopAppService : IApplicationService
    {
        /// <summary>
		/// 获取Shop的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ShopListDto>> GetPaged(GetShopsInput input);


		/// <summary>
		/// 通过指定id获取ShopListDto信息
		/// </summary>
		Task<ShopListDto> GetById(Guid id);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetShopForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Shop的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ShopEditDto> CreateOrUpdate(ShopEditDto input);


        /// <summary>
        /// 删除Shop信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Shop
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<List<ShopListDto>> GetShopList();

        /// <summary>
        /// 获取直营店（用于DropDown）
        /// </summary>
        /// <returns></returns>
        Task<List<ShopListDto>> GetShopListForDropDown();
        Task<ShopListDto> GetShopInfoById(Guid shopId);
        Task<List<OrderDetailDto>> GetExchangeGoodsByIdAsync(Guid orderId, string openId);
        Task<List<ShopListDto>> GetShopAddressAsync();
    }
}
