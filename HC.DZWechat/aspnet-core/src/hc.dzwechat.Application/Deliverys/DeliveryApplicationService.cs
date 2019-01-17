
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
using HC.DZWechat.WechatUsers;

namespace HC.DZWechat.Deliverys
{
    /// <summary>
    /// Delivery应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class DeliveryAppService : DZWechatAppServiceBase, IDeliveryAppService
    {
        private readonly IRepository<Delivery, Guid> _entityRepository;
        private readonly IRepository<WechatUser, Guid> _wechatUserRepository;

        private readonly IDeliveryManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DeliveryAppService(
        IRepository<Delivery, Guid> entityRepository
        , IDeliveryManager entityManager
            , IRepository<WechatUser, Guid> wechatUserRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _wechatUserRepository = wechatUserRepository;
        }


        /// <summary>
        /// 获取Delivery的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<List<DeliveryListDto>> GetNoPaged(GetDeliverysInput input)
        {

            var query = _entityRepository.GetAll().Where(v => v.UserId == input.UserId);
            var entityList = await query
                    .OrderByDescending(v => v.IsDefault).AsNoTracking()
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

        //public async Task CreateOrUpdate(CreateOrUpdateDeliveryInput input)
        //{

        //    if (input.Delivery.Id.HasValue)
        //    {
        //        await Update(input.Delivery);
        //    }
        //    else
        //    {
        //        await Create(input.Delivery);
        //    }
        //}


        /// <summary>
        /// 新增Delivery
        /// </summary>
        [AbpAllowAnonymous]
        protected virtual async Task<DeliveryListDto> Create(DeliveryWXEditDto input)
        {
            var entity = new Delivery();
            entity.UserId = input.UserId;
            entity.Name = input.AddressDetail.Name;
            entity.Phone = input.AddressDetail.Phone;
            entity.IsDefault = input.IsDefault;
            entity.Address = input.AddressDetail.AddressDetail;
            entity.Province = input.Province;
            entity.City = input.City;
            entity.Area = input.Area;
            if (entity.IsDefault == true)
            {
                var result = await _entityRepository.GetAll().Where(v => v.UserId == input.UserId).ToListAsync();
                foreach (var item in result)
                {
                    item.IsDefault = false;
                    await _entityRepository.UpdateAsync(item);
                }
            }
            entity = await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<DeliveryListDto>();
        }

        /// <summary>
        /// 编辑Delivery
        /// </summary>
        [AbpAllowAnonymous]
        protected virtual async Task<DeliveryListDto> Update(DeliveryWXEditDto input)
        {
            var entity = await _entityRepository.GetAsync(input.Id.Value);
            if (input.IsDefault == true)
            {
                var result = await _entityRepository.GetAll().Where(v => v.UserId == input.UserId).ToListAsync();
                foreach (var item in result)
                {
                    item.IsDefault = false;
                    await _entityRepository.UpdateAsync(item);
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            //entity.UserId = input.UserId;
            entity.Name = input.AddressDetail.Name;
            entity.Phone = input.AddressDetail.Phone;
            entity.IsDefault = input.IsDefault;
            entity.Address = input.AddressDetail.AddressDetail;
            entity.Province = input.Province;
            entity.City = input.City;
            entity.Area = input.Area;
            entity = await _entityRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<DeliveryListDto>();
        }



        /// <summary>
        /// 删除Delivery信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [AbpAllowAnonymous]
        public async Task WXDelete(EntityDto<Guid> input)
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
        /// 修改或删除收货地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<DeliveryListDto> WXCreateOrUpdate(DeliveryWXEditDto input)
        {
            Guid userId = await _wechatUserRepository.GetAll().Where(v => v.WxOpenId == input.WxOpenId).Select(v => v.Id).FirstOrDefaultAsync();
            //input.Address = input.Province + input.City + input.Area;
            input.UserId = userId;
            if (input.Id.HasValue)
            {
                return await Update(input);
            }
            else
            {
                return await Create(input);
            }
        }

        /// <summary>
        /// 小程序获取用户收货地址列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<DeliveryListDto>> GetWxDeliveryListAsync(EntityDto<string> input)
        {
            Guid userId = await _wechatUserRepository.GetAll().Where(v => v.WxOpenId == input.Id).Select(v => v.Id).FirstOrDefaultAsync();
            var result = await _entityRepository.GetAll().Where(v => v.UserId == userId).OrderByDescending(v => v.IsDefault).ToListAsync();
            //.Select(v => new DeliveryListDto()
            //{
            //    Id = v.Id,
            //    Name = v.Name,
            //    Phone = v.Phone,
            //    IsDefault = v.IsDefault,
            //    Address = v.Address.Length > 10 ? v.Address.Substring(10)+"..." : v.Address
            //}).OrderByDescending(v => v.IsDefault).ToListAsync();
            return result.MapTo<List<DeliveryListDto>>();
        }

        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<DeliveryListDto> GetWxDeliveryByIdAsync(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<DeliveryListDto>();
        }
    }
}


