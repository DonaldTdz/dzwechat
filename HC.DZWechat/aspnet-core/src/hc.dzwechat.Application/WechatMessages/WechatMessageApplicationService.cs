
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


using HC.DZWechat.WechatMessages;
using HC.DZWechat.WechatMessages.Dtos;




namespace HC.DZWechat.WechatMessages
{
    /// <summary>
    /// WechatMessage应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class WechatMessageAppService : DZWechatAppServiceBase, IWechatMessageAppService
    {
        private readonly IRepository<WechatMessage, Guid> _entityRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public WechatMessageAppService(
        IRepository<WechatMessage, Guid> entityRepository

        )
        {
            _entityRepository = entityRepository;

        }


        /// <summary>
        /// 获取WechatMessage的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<WechatMessageListDto>> GetPaged(GetWechatMessagesInput input)
        {

            var query = _entityRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.MesText), m => m.KeyWord.Contains(input.MesText))
                .WhereIf(input.TriggerType.HasValue, m => m.TriggerType == input.TriggerType)
                .WhereIf(input.MsgType.HasValue, m => m.MsgType == input.MsgType);
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<WechatMessageListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<WechatMessageListDto>>();

            return new PagedResultDto<WechatMessageListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取WechatMessageListDto信息
        /// </summary>

        public async Task<WechatMessageListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<WechatMessageListDto>();
        }

        /// <summary>
        /// 获取编辑 WechatMessage
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetWechatMessageForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetWechatMessageForEditOutput();
            WechatMessageEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<WechatMessageEditDto>();

                //wechatMessageEditDto = ObjectMapper.Map<List<wechatMessageEditDto>>(entity);
            }
            else
            {
                editDto = new WechatMessageEditDto();
            }

            output.WechatMessage = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改WechatMessage的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateWechatMessageInput input)
        {

            if (input.WechatMessage.Id.HasValue)
            {
                await Update(input.WechatMessage);
            }
            else
            {
                await Create(input.WechatMessage);
            }
        }


        /// <summary>
        /// 新增WechatMessage
        /// </summary>

        protected virtual async Task<WechatMessageEditDto> Create(WechatMessageEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <WechatMessage>(input);
            var entity = input.MapTo<WechatMessage>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<WechatMessageEditDto>();
        }

        /// <summary>
        /// 编辑WechatMessage
        /// </summary>

        protected virtual async Task Update(WechatMessageEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除WechatMessage信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除WechatMessage的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出WechatMessage为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        /// <summary>
        /// 添加或者修改WechatMessage的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        public async Task CreateOrUpdateDto(WechatMessageEditDto input)
        {
            if (input.Id.HasValue)
            {
                await Update(input);
            }
            else
            {
                await Create(input);
            }
        }

    }
}


