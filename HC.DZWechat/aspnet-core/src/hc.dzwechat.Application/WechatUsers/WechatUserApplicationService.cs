
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


using HC.DZWechat.WechatUsers;
using HC.DZWechat.WechatUsers.Dtos;
using HC.DZWechat.WechatUsers.DomainService;



namespace HC.DZWechat.WechatUsers
{
    /// <summary>
    /// WechatUser应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class WechatUserAppService : DZWechatAppServiceBase, IWechatUserAppService
    {
        private readonly IRepository<WechatUser, Guid> _entityRepository;

        private readonly IWechatUserManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public WechatUserAppService(
        IRepository<WechatUser, Guid> entityRepository
        , IWechatUserManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取WechatUser的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<WechatUserListDto>> GetPaged(GetWechatUsersInput input)
        {

            var query = _entityRepository.GetAll()
                    .WhereIf(!string.IsNullOrEmpty(input.FilterText), u => u.UserName.Contains(input.FilterText)
                    || u.Phone.Contains(input.FilterText));

            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(v => v.BindStatus).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<WechatUserListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<WechatUserListDto>>();

            return new PagedResultDto<WechatUserListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取WechatUserListDto信息
        /// </summary>

        public async Task<WechatUserListDto> GetById(Guid id)
        {
            var entity = await _entityRepository.GetAsync(id);
            return entity.MapTo<WechatUserListDto>();
        }

        /// <summary>
        /// 获取编辑 WechatUser
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetWechatUserForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetWechatUserForEditOutput();
            WechatUserEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<WechatUserEditDto>();

                //wechatUserEditDto = ObjectMapper.Map<List<wechatUserEditDto>>(entity);
            }
            else
            {
                editDto = new WechatUserEditDto();
            }

            output.WechatUser = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改WechatUser的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateWechatUserInput input)
        {

            if (input.WechatUser.Id.HasValue)
            {
                await Update(input.WechatUser);
            }
            else
            {
                await Create(input.WechatUser);
            }
        }


        /// <summary>
        /// 新增WechatUser
        /// </summary>

        protected virtual async Task<WechatUserEditDto> Create(WechatUserEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <WechatUser>(input);
            var entity = input.MapTo<WechatUser>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<WechatUserEditDto>();
        }

        /// <summary>
        /// 编辑WechatUser
        /// </summary>

        protected virtual async Task Update(WechatUserEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除WechatUser信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除WechatUser的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出WechatUser为excel表,等待开发。
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


