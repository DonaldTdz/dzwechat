
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


using HC.DZWechat.ShopCarts;
using HC.DZWechat.ShopCarts.Dtos;
using HC.DZWechat.ShopCarts.DomainService;
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.WechatUsers;
using HC.DZWechat.Goods;

namespace HC.DZWechat.ShopCarts
{
    /// <summary>
    /// ShopCart应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ShopCartAppService : DZWechatAppServiceBase, IShopCartAppService
    {
        private readonly IRepository<ShopCart, Guid> _entityRepository;
        private readonly IRepository<WechatUser, Guid> _wechatUserRepository;
        private readonly IRepository<Good, Guid> _goodsRepository;

        private readonly IShopCartManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ShopCartAppService(
        IRepository<ShopCart, Guid> entityRepository
        , IRepository<WechatUser, Guid> wechatUserRepository
        , IRepository<Good, Guid> goodsRepository
        , IShopCartManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _wechatUserRepository = wechatUserRepository;
            _goodsRepository = goodsRepository;
            _entityManager =entityManager;
        }


        /// <summary>
        /// 获取ShopCart的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ShopCartListDto>> GetPaged(GetShopCartsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ShopCartListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ShopCartListDto>>();

			return new PagedResultDto<ShopCartListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ShopCartListDto信息
		/// </summary>
		 
		public async Task<ShopCartListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ShopCartListDto>();
		}

		/// <summary>
		/// 获取编辑 ShopCart
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetShopCartForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetShopCartForEditOutput();
ShopCartEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ShopCartEditDto>();

				//shopCartEditDto = ObjectMapper.Map<List<shopCartEditDto>>(entity);
			}
			else
			{
				editDto = new ShopCartEditDto();
			}

			output.ShopCart = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ShopCart的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateShopCartInput input)
		{

			if (input.ShopCart.Id.HasValue)
			{
				await Update(input.ShopCart);
			}
			else
			{
				await Create(input.ShopCart);
			}
		}


		/// <summary>
		/// 新增ShopCart
		/// </summary>
		
		protected virtual async Task<ShopCartEditDto> Create(ShopCartEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ShopCart>(input);
            var entity=input.MapTo<ShopCart>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<ShopCartEditDto>();
		}

		/// <summary>
		/// 编辑ShopCart
		/// </summary>
		
		protected virtual async Task Update(ShopCartEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ShopCart信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ShopCart的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        [AbpAllowAnonymous]
        public async Task AddCartAsync(ShopCartInputDto input)
        {
            var userId = await _wechatUserRepository.GetAll().Where(w => w.WxOpenId == input.WxOpenId).Select(w => w.Id).FirstAsync();
            var goods = await _goodsRepository.GetAsync(input.GoodsId);
            if (await _entityRepository.GetAll().AnyAsync(e => e.UserId == userId && e.GoodsId == input.GoodsId && e.ExchangeCode == input.ExchangeCode))
            {
                var shopCart = _entityRepository.GetAll().Where(e => e.UserId == userId && e.GoodsId == input.GoodsId && e.ExchangeCode == input.ExchangeCode).First();
                var num = shopCart.Num + input.Num;
                if (goods.Stock.HasValue && num > goods.Stock)
                {
                    shopCart.Num = goods.Stock;
                }
                else
                {
                    shopCart.Num = num;
                }
            }
            else
            {
                var shopCart = new ShopCart()
                {
                    GoodsId = goods.Id,
                    ExchangeCode = input.ExchangeCode,
                    Integral = goods.Integral,
                    Num = input.Num,
                    Specification = goods.Specification,
                    Unit = goods.Unit,
                    UserId = userId
                };
                await _entityRepository.InsertAsync(shopCart);
            }
        }

        /// <summary>
        /// 导出ShopCart为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}

