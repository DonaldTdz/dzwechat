
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


using HC.DZWechat.ShopCarts.Dtos;
using HC.DZWechat.ShopCarts;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.ShopCarts
{
    /// <summary>
    /// ShopCart应用层服务的接口方法
    ///</summary>
    public interface IShopCartAppService : IApplicationService
    {
        /// <summary>
		/// 获取ShopCart的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ShopCartListDto>> GetPaged(GetShopCartsInput input);


		/// <summary>
		/// 通过指定id获取ShopCartListDto信息
		/// </summary>
		Task<ShopCartListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetShopCartForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改ShopCart的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateShopCartInput input);


        /// <summary>
        /// 删除ShopCart信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除ShopCart
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出ShopCart为excel表
        /// </summary>
        //Task<FileDto> GetToExcel();

        Task AddCartAsync(ShopCartInputDto input);

        Task<UserCart> GetUserCartListAsync(string wxopenid);

        Task<UserCartDto> GetCheckCartGoodsAsync(Guid id);

        Task UserShopCartDeleteAsync(ShopCartDeleteInput input);
    }
}
