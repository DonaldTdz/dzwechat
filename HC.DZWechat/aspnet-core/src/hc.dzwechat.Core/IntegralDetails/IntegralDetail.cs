using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.IntegralDetails
{
    [Table("IntegralDetails")]
    public class IntegralDetail : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [StringLength(50)]
        [ForeignKey("UserId")]
        public virtual Guid UserId { get; set; }

        /// <summary>
        /// 初始积分
        /// </summary>
        public virtual decimal? InitialIntegral { get; set; }
        /// <summary>
        /// 发生积分（正、负）
        /// </summary>
        public virtual decimal? Integral { get; set; }
        /// <summary>
        /// 结束积分
        /// </summary>
        public virtual decimal? FinalIntegral { get; set; }
        /// <summary>
        /// 积分类型(枚举：绑定会员、每日签到、VIP系统导入、商品兑换)
        /// </summary>
        public virtual IntegralType? Type { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public virtual string Desc { get; set; }
        /// <summary>
        /// 引用Id 多个用逗号隔开
        /// </summary>
        [StringLength(500)]
        public virtual string RefId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
