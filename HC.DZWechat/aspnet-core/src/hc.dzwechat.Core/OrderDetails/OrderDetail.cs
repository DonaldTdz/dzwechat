using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.OrderDetails
{
    [Table("OrderDetails")]
    public class OrderDetail : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 订单Id 外键
        /// </summary>
        [Required]
        public virtual Guid OrderId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        [Required]
        public virtual Guid GoodsId { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        [StringLength(50)]
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
        /// 兑换状态（枚举：1、未兑换 2、已兑换）
        /// </summary>
        public virtual ExchangeStatus? Status { get; set; }
        /// <summary>
        /// 兑换时间
        /// </summary>
        public virtual DateTime? ExchangeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        public virtual ExchangeCodeEnum exchangeCode { get; set; }
    }

}
