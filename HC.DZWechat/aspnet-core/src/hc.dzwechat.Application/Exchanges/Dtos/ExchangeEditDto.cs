
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Exchanges;

namespace HC.DZWechat.Exchanges.Dtos
{
    public class ExchangeEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }



        /// <summary>
        /// OrderDetailId
        /// </summary>
        public Guid? OrderDetailId { get; set; }



        /// <summary>
        /// ExchangeCode
        /// </summary>
        public int? ExchangeCode { get; set; }


        public Guid? WechatUserId { get; set; }
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



        /// <summary>
        /// LogisticsNo
        /// </summary>
        public string LogisticsNo { get; set; }




    }
}