
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


using HC.DZWechat.Goods;
using HC.DZWechat.Goods.Dtos;
using HC.DZWechat.Goods.DomainService;



namespace HC.DZWechat.Goods
{
    /// <summary>
    /// Good应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class GoodAppService : DZWechatAppServiceBase, IGoodAppService
    {
        private readonly IRepository<Good, Guid> _entityRepository;

        private readonly IGoodManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public GoodAppService(
        IRepository<Good, Guid> entityRepository
        ,IGoodManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取Good的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<GoodListDto>> GetPaged(GetGoodsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<GoodListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<GoodListDto>>();

			return new PagedResultDto<GoodListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取GoodListDto信息
		/// </summary>
		 
		public async Task<GoodListDto> GetById(Guid id)
		{
			var entity = await _entityRepository.GetAsync(id);

		    return entity.MapTo<GoodListDto>();
		}

		/// <summary>
		/// 获取编辑 Good
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetGoodForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetGoodForEditOutput();
GoodEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<GoodEditDto>();

				//goodEditDto = ObjectMapper.Map<List<goodEditDto>>(entity);
			}
			else
			{
				editDto = new GoodEditDto();
			}

			output.Good = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Good的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateGoodInput input)
		{

			if (input.Good.Id.HasValue)
			{
				await Update(input.Good);
			}
			else
			{
				await Create(input.Good);
			}
		}


		/// <summary>
		/// 新增Good
		/// </summary>
		
		protected virtual async Task<GoodEditDto> Create(GoodEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Good>(input);
            var entity=input.MapTo<Good>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<GoodEditDto>();
		}

		/// <summary>
		/// 编辑Good
		/// </summary>
		
		protected virtual async Task Update(GoodEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Good信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Good的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出Good为excel表,等待开发。
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


