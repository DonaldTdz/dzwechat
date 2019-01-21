using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HC.DZWechat.VipUsers
{
    [Table("VipUsers")]
    public class VipUser : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 接口数据code
        /// </summary>
        [StringLength(50)]
        public virtual string VipCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(20)]
        public virtual string Phone { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(50)]
        public virtual string IdNumber { get; set; }
        /// <summary>
        /// 购买金额
        /// </summary>
        public virtual decimal? PurchaseAmount { get; set; }
        /// <summary>
        /// 申请创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
