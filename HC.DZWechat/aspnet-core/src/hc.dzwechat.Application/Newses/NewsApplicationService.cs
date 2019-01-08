
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


using HC.DZWechat.Newses;
using HC.DZWechat.Newses.Dtos;
using HC.DZWechat.Newses.DomainService;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Newses
{
    /// <summary>
    /// News应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class NewsAppService : DZWechatAppServiceBase, INewsAppService
    {
        private readonly IRepository<News, Guid> _entityRepository;

        private readonly INewsManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public NewsAppService(
        IRepository<News, Guid> entityRepository
        , INewsManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取News的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<NewsListDto>> GetPaged(GetNewssInput input)
        {

            var query = _entityRepository.GetAll().WhereIf(input.NewsType.HasValue, n => n.Type == input.NewsType);
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<NewsListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<NewsListDto>>();

            return new PagedResultDto<NewsListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取NewsListDto信息
        /// </summary>
        public async Task<NewsListDto> GetById(EntityDto<Guid> input)
        {
            //var entity = await _entityRepository.GetAsync(input.Id);
            var entity = await _entityRepository.GetAll().Where(n => n.Id == input.Id).FirstOrDefaultAsync();

            return entity.MapTo<NewsListDto>();
        }

        /// <summary>
        /// 获取编辑 News
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetNewsForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetNewsForEditOutput();
            NewsEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<NewsEditDto>();

                //newsEditDto = ObjectMapper.Map<List<newsEditDto>>(entity);
            }
            else
            {
                editDto = new NewsEditDto();
            }

            output.News = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改News的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(CreateOrUpdateNewsInput input)
        {

            if (input.News.Id.HasValue)
            {
                await Update(input.News);
            }
            else
            {
                await Create(input.News);
            }
        }


        /// <summary>
        /// 新增News
        /// </summary>
        protected virtual async Task<NewsEditDto> Create(NewsEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <News>(input);
            var entity = input.MapTo<News>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<NewsEditDto>();
        }

        /// <summary>
        /// 编辑News
        /// </summary>
        protected virtual async Task<NewsEditDto> Update(NewsEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
            return entity.MapTo<NewsEditDto>();
        }



        /// <summary>
        /// 删除News信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除News的方法
        /// </summary>
        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出News为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}


        public async Task<NewsListDto> GetByIdAndType(EntityDto<Guid> Input)
        {
            //var entity = await _entityRepository.GetAsync(input.Id);
            var entity = await _entityRepository.GetAll().Where(n => n.Id == Input.Id).FirstOrDefaultAsync();
            return entity.MapTo<NewsListDto>();
        }

        /// <summary>
		/// 添加或者修改News的公共方法
		/// </summary>
		public async Task<NewsEditDto> CreateOrUpdateDto(NewsEditDto input)
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

        [AbpAllowAnonymous]
        public async Task<List<NewsListDto>> GetNewsByTypeAsync(NewsType newsType)
        {
            var entity = await _entityRepository.GetAll().Where(n => n.Type == newsType && n.PushStatus == PushType.已发布).OrderByDescending(r => r.PushTime).ToListAsync();
            return entity.MapTo<List<NewsListDto>>();
        }
    }

}


