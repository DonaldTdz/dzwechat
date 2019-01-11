

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Exchanges;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Exchanges.Dtos
{
    public class ExchangeListDto : EntityDto<Guid>, IHasCreationTime
    {


        /// <summary>
        /// OrderDetailId
        /// </summary>
        public Guid? OrderDetailId { get; set; }



        /// <summary>
        /// ExchangeCode
        /// </summary>
        public ExchangeCodeEnum? ExchangeCode { get; set; }



        /// <summary>
        /// ShopId
        /// </summary>
        public Guid? ShopId { get; set; }



        /// <summary>
        /// UserId
        /// </summary>
        public long? UserId { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }



        /// <summary>
        /// LogisticsCompany
        /// </summary>
        public string LogisticsCompany { get; set; }


        public string WechatUserId { get; set; }

        /// <summary>
        /// LogisticsNo
        /// </summary>
        public string LogisticsNo { get; set; }

        public string ExchangeCodeName
        {
            get
            {
                return ExchangeCode.ToString();
            }
        }

        /// <summary>
        /// 店铺名
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }
    }
}