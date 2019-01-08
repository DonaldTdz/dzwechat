

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Deliverys;

namespace HC.DZWechat.Deliverys.Dtos
{
    public class DeliveryListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }


        public Guid UserId { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }



		/// <summary>
		/// Address
		/// </summary>
		public string Address { get; set; }



		/// <summary>
		/// IsDefault
		/// </summary>
		public bool? IsDefault { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }




    }
}