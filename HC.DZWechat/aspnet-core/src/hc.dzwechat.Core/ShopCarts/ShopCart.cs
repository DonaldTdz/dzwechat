using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.DZEnums.DZCommonEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.DZWechat.ShopCarts
{
    [Table("ShopCarts")]
    public class ShopCart : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual Guid? UserId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [Required]
        public virtual Guid GoodsId { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Specification { get; set; }
        /// <summary>
        /// 商品所需积分快照
        /// </summary>
        public virtual decimal? Integral { get; set; }
        /// <summary>
        /// 商品单位 快照
        /// </summary>
        [StringLength(30)]
        public virtual string Unit { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public virtual decimal? Num { get; set; }
        /// <summary>
        /// 兑换方式
        /// </summary>
        public virtual ExchangeCode ExchangeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }

}
