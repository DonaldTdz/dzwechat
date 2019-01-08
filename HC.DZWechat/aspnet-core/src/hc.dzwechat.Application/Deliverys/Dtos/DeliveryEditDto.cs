
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Deliverys;

namespace  HC.DZWechat.Deliverys.Dtos
{
    public class DeliveryEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


        public Guid UserId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }



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