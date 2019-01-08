
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace  HC.DZWechat.IntegralDetails.Dtos
{
    public class IntegralDetailEditDto :IHasCreationTime
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }



        /// <summary>
        /// InitialIntegral
        /// </summary>
        public decimal? InitialIntegral { get; set; }



		/// <summary>
		/// Integral
		/// </summary>
		public decimal? Integral { get; set; }



		/// <summary>
		/// FinalIntegral
		/// </summary>
		public decimal? FinalIntegral { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		public IntegralType? Type { get; set; }



		/// <summary>
		/// Desc
		/// </summary>
		public string Desc { get; set; }



		/// <summary>
		/// RefId
		/// </summary>
		public string RefId { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}