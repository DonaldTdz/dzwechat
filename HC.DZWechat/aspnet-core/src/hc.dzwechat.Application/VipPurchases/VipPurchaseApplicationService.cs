
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


using HC.DZWechat.VipPurchases;
using HC.DZWechat.VipPurchases.Dtos;
using HC.DZWechat.VipPurchases.DomainService;



namespace HC.DZWechat.VipPurchases
{
    /// <summary>
    /// VipPurchase应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class VipPurchaseAppService : DZWechatAppServiceBase, IVipPurchaseAppService
    {
        private readonly IRepository<VipPurchase, Guid> _entityRepository;

        private readonly IVipPurchaseManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public VipPurchaseAppService(
        IRepository<VipPurchase, Guid> entityRepository
        , IVipPurchaseManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取VipPurchase的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<VipPurchaseListDto>> GetPaged(GetVipPurchasesInput input)
        {
            var query = _entityRepository.GetAll().Where(v => v.VipUserId == input.VipUserId);
            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<VipPurchaseListDto>>();
            return new PagedResultDto<VipPurchaseListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取VipPurchaseListDto信息
        /// </summary>

        public async Task<VipPurchaseListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<VipPurchaseListDto>();
        }

        /// <summary>
        /// 获取编辑 VipPurchase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetVipPurchaseForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetVipPurchaseForEditOutput();
            VipPurchaseEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<VipPurchaseEditDto>();

                //vipPurchaseEditDto = ObjectMapper.Map<List<vipPurchaseEditDto>>(entity);
            }
            else
            {
                editDto = new VipPurchaseEditDto();
            }

            output.VipPurchase = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改VipPurchase的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateVipPurchaseInput input)
        {

            if (input.VipPurchase.Id.HasValue)
            {
                await Update(input.VipPurchase);
            }
            else
            {
                await Create(input.VipPurchase);
            }
        }


        /// <summary>
        /// 新增VipPurchase
        /// </summary>

        protected virtual async Task<VipPurchaseEditDto> Create(VipPurchaseEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <VipPurchase>(input);
            var entity = input.MapTo<VipPurchase>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<VipPurchaseEditDto>();
        }

        /// <summary>
        /// 编辑VipPurchase
        /// </summary>

        protected virtual async Task Update(VipPurchaseEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除VipPurchase信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除VipPurchase的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出VipPurchase为excel表,等待开发。
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


