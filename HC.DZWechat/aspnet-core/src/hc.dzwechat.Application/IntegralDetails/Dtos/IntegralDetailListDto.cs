

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.IntegralDetails.Dtos
{
    public class IntegralDetailListDto : EntityDto<Guid>, IHasCreationTime
    {


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
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }




    }

    public class SignInDto
    {
        public Guid UserId { get; set; }
        public bool IsSign { get; set; }
        public decimal Integral { get; set; }
    }
    public class WXIntegralDetailListDto {
        public decimal? Integral { get; set; }
        public DateTime CreationTime { get; set; }
        public IntegralType? Type { get; set; }

        public string TypeName
        {
            get
            {
               return Type.ToString();
            }
        }
        public string TimeFormat
        {
            get
            {
                return CreationTime.ToString();
            }
        }
    }

}