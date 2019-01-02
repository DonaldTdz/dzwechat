
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


using HC.DZWechat.IntegralDetails;
using HC.DZWechat.IntegralDetails.Dtos;
using HC.DZWechat.IntegralDetails.DomainService;



namespace HC.DZWechat.IntegralDetails
{
    /// <summary>
    /// IntegralDetail应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class IntegralDetailAppService : DZWechatAppServiceBase, IIntegralDetailAppService
    {
        private readonly IRepository<IntegralDetail, Guid> _entityRepository;

        private readonly IIntegralDetailManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public IntegralDetailAppService(
        IRepository<IntegralDetail, Guid> entityRepository
        ,IIntegralDetailManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取IntegralDetail的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<IntegralDetailListDto>> GetPaged(GetIntegralDetailsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<IntegralDetailListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<IntegralDetailListDto>>();

			return new PagedResultDto<IntegralDetailListDto>(count,entityListDtos);
		}

        public async Task<PagedResultDto<IntegralDetailListDto>> GetPagedById(GetIntegralDetailsInput input)
        {

            var query = _entityRepository.GetAll().Where(v => v.UnionId == input.UnionId);
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<IntegralDetailListDto>>();
            return new PagedResultDto<IntegralDetailListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 通过指定id获取IntegralDetailListDto信息
        /// </summary>

        public async Task<IntegralDetailListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<IntegralDetailListDto>();
		}

		/// <summary>
		/// 获取编辑 IntegralDetail
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetIntegralDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetIntegralDetailForEditOutput();
IntegralDetailEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<IntegralDetailEditDto>();

				//integralDetailEditDto = ObjectMapper.Map<List<integralDetailEditDto>>(entity);
			}
			else
			{
				editDto = new IntegralDetailEditDto();
			}

			output.IntegralDetail = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改IntegralDetail的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateIntegralDetailInput input)
		{

			if (input.IntegralDetail.Id.HasValue)
			{
				await Update(input.IntegralDetail);
			}
			else
			{
				await Create(input.IntegralDetail);
			}
		}


		/// <summary>
		/// 新增IntegralDetail
		/// </summary>
		
		protected virtual async Task<IntegralDetailEditDto> Create(IntegralDetailEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <IntegralDetail>(input);
            var entity=input.MapTo<IntegralDetail>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<IntegralDetailEditDto>();
		}

		/// <summary>
		/// 编辑IntegralDetail
		/// </summary>
		
		protected virtual async Task Update(IntegralDetailEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除IntegralDetail信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除IntegralDetail的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出IntegralDetail为excel表,等待开发。
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


