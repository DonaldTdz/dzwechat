

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Exchanges;
using HC.DZWechat.DZEnums.DZCommonEnums;
using Abp.AutoMapper;

namespace HC.DZWechat.Exchanges.Dtos
{
    [AutoMapFrom(typeof(Exchange))]
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
        /// µêÆÌÃû
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// ¶©µ¥±àºÅ
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// ¹æ¸ñ
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// ¶©µ¥id
        /// </summary>
        public Guid OrderId { get; set; }

        public string DeliveryName { get; set; }
        public string DeliveryPhone { get; set; }
    }
}