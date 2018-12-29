
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


using HC.DZWechat.WechatSubscribes;
using HC.DZWechat.WechatSubscribes.Dtos;
using HC.DZWechat.WechatSubscribes.DomainService;



namespace HC.DZWechat.WechatSubscribes
{
    /// <summary>
    /// WechatSubscribe应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class WechatSubscribeAppService : DZWechatAppServiceBase, IWechatSubscribeAppService
    {
        private readonly IRepository<WechatSubscribe, Guid> _entityRepository;

        private readonly IWechatSubscribeManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public WechatSubscribeAppService(
        IRepository<WechatSubscribe, Guid> entityRepository
        ,IWechatSubscribeManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取WechatSubscribe的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<WechatSubscribeListDto>> GetPaged(GetWechatSubscribesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<WechatSubscribeListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<WechatSubscribeListDto>>();

			return new PagedResultDto<WechatSubscribeListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取WechatSubscribeListDto信息
		/// </summary>
		 
		public async Task<WechatSubscribeListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<WechatSubscribeListDto>();
		}

		/// <summary>
		/// 获取编辑 WechatSubscribe
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetWechatSubscribeForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetWechatSubscribeForEditOutput();
WechatSubscribeEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<WechatSubscribeEditDto>();

				//wechatSubscribeEditDto = ObjectMapper.Map<List<wechatSubscribeEditDto>>(entity);
			}
			else
			{
				editDto = new WechatSubscribeEditDto();
			}

			output.WechatSubscribe = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改WechatSubscribe的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateWechatSubscribeInput input)
		{

			if (input.WechatSubscribe.Id.HasValue)
			{
				await Update(input.WechatSubscribe);
			}
			else
			{
				await Create(input.WechatSubscribe);
			}
		}


		/// <summary>
		/// 新增WechatSubscribe
		/// </summary>
		
		protected virtual async Task<WechatSubscribeEditDto> Create(WechatSubscribeEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <WechatSubscribe>(input);
            var entity=input.MapTo<WechatSubscribe>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<WechatSubscribeEditDto>();
		}

		/// <summary>
		/// 编辑WechatSubscribe
		/// </summary>
		
		protected virtual async Task Update(WechatSubscribeEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除WechatSubscribe信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除WechatSubscribe的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出WechatSubscribe为excel表,等待开发。
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


