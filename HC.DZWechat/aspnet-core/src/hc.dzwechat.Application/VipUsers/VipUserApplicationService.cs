
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


using HC.DZWechat.VipUsers;
using HC.DZWechat.VipUsers.Dtos;
using HC.DZWechat.VipUsers.DomainService;
using HC.DZWechat.WechatUsers;

namespace HC.DZWechat.VipUsers
{
    /// <summary>
    /// VipUser应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class VipUserAppService : DZWechatAppServiceBase, IVipUserAppService
    {
        private readonly IRepository<VipUser, Guid> _entityRepository;
        private readonly IRepository<WechatUser, Guid> _wechatUserRepository;
        private readonly IVipUserManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public VipUserAppService(
        IRepository<VipUser, Guid> entityRepository
        , IRepository<WechatUser, Guid> wechatUserRepository
        , IVipUserManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _wechatUserRepository = wechatUserRepository;
             _entityManager =entityManager;
        }


        /// <summary>
        /// 获取VipUser的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<VipUserListDto>> GetPaged(GetVipUsersInput input)
		{
            var user = _wechatUserRepository.GetAll().Where(v => v.UserType == DZEnums.DZCommonEnums.UserType.Vip会员);
		    var query = _entityRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.FilterText),v=>v.IdNumber.Contains(input.FilterText)||v.Name.Contains(input.FilterText)||v.Phone.Contains(input.FilterText));
            var result = from q in query
                         join u in user on q.Id equals u.VipUserId into table
                         from t in table.DefaultIfEmpty()
                         select new VipUserListDto()
                         {
                             Id = q.Id,
                             Name = q.Name,
                             IdNumber = q.IdNumber,
                             BindWechatUser = t.NickName??null,
                             Phone = q.Phone,
                             CreationTime = q.CreationTime,
                             PurchaseAmount = q.PurchaseAmount,
                             WechatId = t.Id
                         };
            var count = await result.CountAsync();
            var entityList = await result
                    .OrderByDescending(v=>v.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
			return new PagedResultDto<VipUserListDto>(count, entityList);
		}


		/// <summary>
		/// 通过指定id获取VipUserListDto信息
		/// </summary>
		 
		public async Task<VipUserListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);
		    return entity.MapTo<VipUserListDto>();
		}

		/// <summary>
		/// 获取编辑 VipUser
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetVipUserForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetVipUserForEditOutput();
VipUserEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<VipUserEditDto>();

				//vipUserEditDto = ObjectMapper.Map<List<vipUserEditDto>>(entity);
			}
			else
			{
				editDto = new VipUserEditDto();
			}

			output.VipUser = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改VipUser的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateVipUserInput input)
		{

			if (input.VipUser.Id.HasValue)
			{
				await Update(input.VipUser);
			}
			else
			{
				await Create(input.VipUser);
			}
		}


		/// <summary>
		/// 新增VipUser
		/// </summary>
		
		protected virtual async Task<VipUserEditDto> Create(VipUserEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <VipUser>(input);
            var entity=input.MapTo<VipUser>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<VipUserEditDto>();
		}

		/// <summary>
		/// 编辑VipUser
		/// </summary>
		
		protected virtual async Task Update(VipUserEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除VipUser信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除VipUser的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// VIP认证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<bool> BindVipUser(GetWXVipUserInput input)
        {
            var vipUser =await _entityRepository.GetAll().Where(v => v.Phone == input.Phone && v.IdNumber == input.IdNumber).FirstOrDefaultAsync();
            if(vipUser != null)
            {
               Guid userId = await _wechatUserRepository.GetAll().Where(v => v.WxOpenId == input.WxOpenId).Select(v => v.Id).FirstAsync();
               var entity = await _wechatUserRepository.GetAsync(userId);
                entity.AuthTime = DateTime.Now;
                entity.UserType = DZEnums.DZCommonEnums.UserType.Vip会员;
                entity.VipUserId = vipUser.Id;
                await _wechatUserRepository.UpdateAsync(entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询vip信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<VipUserListDto> GetVipUserById(GetWXVipUserInput input)
        {
            Guid? vipId = await _wechatUserRepository.GetAll().Where(v => v.WxOpenId == input.WxOpenId).Select(v => v.VipUserId).FirstAsync();
            var entity = await _entityRepository.GetAsync(vipId.Value);
            var result = entity.MapTo<VipUserListDto>();
            if (!string.IsNullOrEmpty(result.Phone))
            {
                string tempPhone = result.Phone;
                result.Phone = tempPhone.Substring(0, 3) + "****" + result.Phone.Substring(7, 4);
            }
            return result;
        }
    }
}


