
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


using HC.DZWechat.Orders;
using HC.DZWechat.Orders.Dtos;
using HC.DZWechat.Orders.DomainService;



namespace HC.DZWechat.Orders
{
    /// <summary>
    /// Order应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class OrderAppService : DZWechatAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _entityRepository;

        private readonly IOrderManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public OrderAppService(
        IRepository<Order, Guid> entityRepository
        ,IOrderManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取Order的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<OrderListDto>> GetPaged(GetOrdersInput input)
		{

		    var query = _entityRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.FilterText), u => u.Phone.Contains(input.FilterText));
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<OrderListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<OrderListDto>>();

			return new PagedResultDto<OrderListDto>(count,entityListDtos);
		}

        public async Task<PagedResultDto<OrderListDto>> GetPagedById(GetOrdersInput input)
        {

            var query = _entityRepository.GetAll().Where(v=>v.UserId == input.Id);
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v=>v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<OrderListDto>>();

            return new PagedResultDto<OrderListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 通过指定id获取OrderListDto信息
        /// </summary>

        public async Task<OrderListDto> GetById(Guid id)
		{
			var entity = await _entityRepository.GetAsync(id);
		    return entity.MapTo<OrderListDto>();
		}

		/// <summary>
		/// 获取编辑 Order
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetOrderForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetOrderForEditOutput();
OrderEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<OrderEditDto>();

				//orderEditDto = ObjectMapper.Map<List<orderEditDto>>(entity);
			}
			else
			{
				editDto = new OrderEditDto();
			}

			output.Order = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Order的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateOrderInput input)
		{

			if (input.Order.Id.HasValue)
			{
				await Update(input.Order);
			}
			else
			{
				await Create(input.Order);
			}
		}


		/// <summary>
		/// 新增Order
		/// </summary>
		
		protected virtual async Task<OrderEditDto> Create(OrderEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Order>(input);
            var entity=input.MapTo<Order>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<OrderEditDto>();
		}

		/// <summary>
		/// 编辑Order
		/// </summary>
		
		protected virtual async Task Update(OrderEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Order信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Order的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出Order为excel表,等待开发。
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


