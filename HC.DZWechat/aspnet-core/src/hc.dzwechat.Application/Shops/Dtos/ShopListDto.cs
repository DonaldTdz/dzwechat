

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Shops;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Shops.Dtos
{
    public class ShopListDto : EntityDto<Guid>, IHasCreationTime
    {


        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Name不能为空")]
        public string Name { get; set; }



        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }



        /// <summary>
        /// Type
        /// </summary>
        public ShopTypeEnum? Type { get; set; }
        public string TypeName
        {
            get
            {
                return Type.ToString();
            }
        }


        /// <summary>
        /// Tel
        /// </summary>
        public string Tel { get; set; }



        /// <summary>
        /// Desc
        /// </summary>
        public string Desc { get; set; }



        /// <summary>
        /// RetailerId
        /// </summary>
        public Guid? RetailerId { get; set; }



        /// <summary>
        /// CoverPhoto
        /// </summary>
        public string CoverPhoto { get; set; }



        /// <summary>
        /// Longitude
        /// </summary>
        public decimal? Longitude { get; set; }



        /// <summary>
        /// Latitude
        /// </summary>
        public decimal? Latitude { get; set; }



        /// <summary>
        /// VerificationCode
        /// </summary>
        public string VerificationCode { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }
    }
}