
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
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using HC.DZWechat.Shops.Dtos;
using HC.DZWechat.Shops.DomainService;
using System.Text;
using HC.DZWechat.ScanExchange;
using HC.DZWechat.ScanExchange.Dtos;

namespace HC.DZWechat.Shops
{
    /// <summary>
    /// Shop应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ShopAppService : DZWechatAppServiceBase, IShopAppService
    {
        private readonly IRepository<Shop, Guid> _entityRepository;

        private readonly IShopManager _entityManager;
        private readonly IScanExchangeManager _scanExchangeManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ShopAppService(
        IRepository<Shop, Guid> entityRepository
        ,IShopManager entityManager
            , IScanExchangeManager scanExchangeManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager = entityManager;
            _scanExchangeManager = scanExchangeManager;
        }


        /// <summary>
        /// 获取Shop的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ShopListDto>> GetPaged(GetShopsInput input)
		{
		    var query = _entityRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.FilterText), u => u.Name.Contains(input.FilterText));
            var count = await query.CountAsync();
			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
			var entityListDtos =entityList.MapTo<List<ShopListDto>>();
			return new PagedResultDto<ShopListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ShopListDto信息
		/// </summary>
		 
		public async Task<ShopListDto> GetById(Guid id)
		{
			var entity = await _entityRepository.GetAsync(id);
		    return entity.MapTo<ShopListDto>();
		}

		/// <summary>
		/// 获取编辑 Shop
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetShopForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetShopForEditOutput();
ShopEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ShopEditDto>();

				//ShopEditDto = ObjectMapper.Map<List<ShopEditDto>>(entity);
			}
			else
			{
				editDto = new ShopEditDto();
			}

			output.Shop = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Shop的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<ShopEditDto> CreateOrUpdate(ShopEditDto input)
		{

			if (input.Id.HasValue)
			{
				return await Update(input);
			}
			else
			{
                return await Create(input);
			}
		}


		/// <summary>
		/// 新增Shop
		/// </summary>
		
		protected virtual async Task<ShopEditDto> Create(ShopEditDto input)
		{
            var entity=input.MapTo<Shop>();
            entity.VerificationCode = GenerateRandomCode(8);
            await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<ShopEditDto>();
        }

		/// <summary>
		/// 编辑Shop
		/// </summary>
		
		protected virtual async Task<ShopEditDto> Update(ShopEditDto input)
		{
			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
		    await _entityRepository.UpdateAsync(entity);
            return entity.MapTo<ShopEditDto>();
		}



		/// <summary>
		/// 删除Shop信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Shop的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        ///生成制定位数的随机码（数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }

        [AbpAllowAnonymous]
        public async Task<List<ShopListDto>> GetShopList()
        {
            var list = await _entityRepository.GetAll().Select(v => new ShopListDto()
            {
                Id = v.Id,
                Name = v.Name
            }).OrderByDescending(v=>v.Name).ToListAsync();
            return list;
        }

        /// <summary>
        /// 微信获取店铺信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<ShopListDto> GetShopInfoById(Guid shopId)
        {
            var result = await _entityRepository.GetAll().Where(v => v.Id == shopId).Select(v => new ShopListDto()
            {
                Id = v.Id,
                Name = v.Name,
                Address = v.Address
            }).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<OrderDetailDto>> GetExchangeGoodsByIdAsync(Guid orderId, string openId)
        {
            var result = await _scanExchangeManager.GetExchangeGoodsByIdAsync(orderId,openId);
            return result;
        }
    }
}


