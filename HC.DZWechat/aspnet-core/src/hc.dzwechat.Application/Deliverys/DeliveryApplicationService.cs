
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


using HC.DZWechat.Deliverys;
using HC.DZWechat.Deliverys.Dtos;
using HC.DZWechat.Deliverys.DomainService;



namespace HC.DZWechat.Deliverys
{
    /// <summary>
    /// Delivery应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class DeliveryAppService : DZWechatAppServiceBase, IDeliveryAppService
    {
        private readonly IRepository<Delivery, Guid> _entityRepository;

        private readonly IDeliveryManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DeliveryAppService(
        IRepository<Delivery, Guid> entityRepository
        ,IDeliveryManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取Delivery的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<List<DeliveryListDto>> GetNoPaged(GetDeliverysInput input)
		{

		    var query = _entityRepository.GetAll().Where(v=>v.UserId == input.UserId);
			var entityList = await query
					.OrderByDescending(v=>v.IsDefault).AsNoTracking()
					.ToListAsync();
			var entityListDtos = entityList.MapTo<List<DeliveryListDto>>();
            return entityListDtos;
        }


		/// <summary>
		/// 通过指定id获取DeliveryListDto信息
		/// </summary>
		 
		public async Task<DeliveryListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<DeliveryListDto>();
		}

		/// <summary>
		/// 获取编辑 Delivery
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetDeliveryForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetDeliveryForEditOutput();
DeliveryEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<DeliveryEditDto>();

				//deliveryEditDto = ObjectMapper.Map<List<deliveryEditDto>>(entity);
			}
			else
			{
				editDto = new DeliveryEditDto();
			}

			output.Delivery = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Delivery的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateDeliveryInput input)
		{

			if (input.Delivery.Id.HasValue)
			{
				await Update(input.Delivery);
			}
			else
			{
				await Create(input.Delivery);
			}
		}


		/// <summary>
		/// 新增Delivery
		/// </summary>
		
		protected virtual async Task<DeliveryEditDto> Create(DeliveryEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Delivery>(input);
            var entity=input.MapTo<Delivery>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<DeliveryEditDto>();
		}

		/// <summary>
		/// 编辑Delivery
		/// </summary>
		
		protected virtual async Task Update(DeliveryEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Delivery信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Delivery的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出Delivery为excel表,等待开发。
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


