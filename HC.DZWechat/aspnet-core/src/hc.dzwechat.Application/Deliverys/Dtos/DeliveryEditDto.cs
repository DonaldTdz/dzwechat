
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Deliverys;

namespace  HC.DZWechat.Deliverys.Dtos
{
    public class DeliveryEditDto:IHasCreationTime
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


                public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }

    [AutoMapTo(typeof(Delivery))]
    public class DeliveryWXEditDto : IHasCreationTime
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


        public Guid UserId { get; set; }

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


        public AddressDetailDto AddressDetail { get; set; }

        public string WxOpenId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }
    public class AddressDetailDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AddressDetail { get; set; }
    }
}