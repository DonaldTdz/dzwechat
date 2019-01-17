using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Orders
{
    [Table("Orders")]
    public class Order : Entity<Guid>,IHasCreationTime, ICreationAudited //注意修改主键Id数据类型
    {
        /// <summary>
        /// 订单编号（生成唯一格式，需考虑并发）
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string Number { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public virtual Guid? UserId { get; set; }
        public virtual string NickName { get; set; }
        /// <summary>
        /// 会员手机号
        /// </summary>
        [StringLength(20)]
        public virtual string Phone { get; set; }
        /// <summary>
        /// 订单总共所需积分 
        /// </summary>
        public virtual decimal? Integral { get; set; }
        /// <summary>
        /// 订单状态（枚举：1、待支付  2、已支付 2、已完成  3、已取消）
        /// </summary>
        public virtual OrderStatus? Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(50)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 已支付时间
        /// </summary>
        public virtual DateTime? PayTime { get; set; }
        /// <summary>
        /// 已完成时间
        /// </summary>
        public virtual DateTime? CompleteTime { get; set; }
        /// <summary>
        /// 取消时间
        /// </summary>
        public virtual DateTime? CancelTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long? CreatorUserId { get; set; }
        /// <summary>
        /// 收货人姓名 快照
        /// </summary>
        [StringLength(50)]
        public virtual string DeliveryName { get; set; }
        /// <summary>
        /// 收货人电话 快照
        /// </summary>
        [StringLength(20)]
        public virtual string DeliveryPhone { get; set; }
        /// <summary>
        /// 收货人地址 快照
        /// </summary>
        [StringLength(500)]
        public virtual string DeliveryAddress { get; set; }
    }

}
