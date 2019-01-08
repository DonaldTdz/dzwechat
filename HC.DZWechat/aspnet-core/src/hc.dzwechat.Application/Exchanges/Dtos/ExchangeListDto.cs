

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Exchanges;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Exchanges.Dtos
{
    public class ExchangeListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// OrderDetailId
		/// </summary>
		public Guid? OrderDetailId { get; set; }



		/// <summary>
		/// ExchangeCode
		/// </summary>
		public ExchangeCode? ExchangeCode { get; set; }



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


        public string UnionId { get; set; }

        /// <summary>
        /// LogisticsNo
        /// </summary>
        public string LogisticsNo { get; set; }




    }
}