using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.DZEnums.DZCommonEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HC.DZWechat.VipPurchases
{
    [Table("VipPurchases")]
    public class VipPurchase : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// VIP用户Id
        /// </summary>
        [Required]
        public virtual Guid VipUserId { get; set; }
        /// <summary>
        /// 接口数据code
        /// </summary>
        [StringLength(50)]
        public virtual string VipCode { get; set; }
        /// <summary>
        /// 购买金额
        /// </summary>
        public virtual decimal? PurchaseAmount { get; set; }
        /// <summary>
        /// 是否已转换
        /// </summary>
        public virtual bool? IsConvert { get; set; }
        /// <summary>
        /// 转换时间
        /// </summary>
        public virtual DateTime? ConvertTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
