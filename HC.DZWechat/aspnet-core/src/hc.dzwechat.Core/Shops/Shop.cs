using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.DZEnums.DZCommonEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace HC.DZWechat.Shops
{
    [Table("Shops")]
    public class Shop : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>
        [StringLength(200)]
        public virtual string Address { get; set; }
        /// <summary>
        /// 店铺类型（枚举：直营店 目前只支持直营店）
        /// </summary>
        public virtual ShopTypeEnum? Type { get; set; }
        /// <summary>
        /// 店铺电话
        /// </summary>
        [StringLength(20)]
        public virtual string Tel { get; set; }
        /// <summary>
        /// 店铺描述
        /// </summary>
        [StringLength(500)]
        public virtual string Desc { get; set; }
        /// <summary>
        /// 零售客户Id 外键，保留 暂不维护
        /// </summary>
        public virtual Guid? RetailerId { get; set; }
        /// <summary>
        /// 店铺形象图片
        /// </summary>
        [StringLength(500)]
        public virtual string CoverPhoto { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public virtual decimal? Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public virtual decimal? Latitude { get; set; }
        /// <summary>
        /// 店铺验证码（系统根据规则自动生成）
        /// </summary>
        [StringLength(20)]
        public virtual string VerificationCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
