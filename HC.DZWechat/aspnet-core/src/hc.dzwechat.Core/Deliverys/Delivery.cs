using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.DZWechat.Deliverys
{
    [Table("Deliverys")]
    public class Delivery : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [StringLength(50)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        [ForeignKey("UserId")]
        public virtual Guid UserId { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [StringLength(20)]
        public virtual string Phone { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        [StringLength(500)]
        public virtual string Address { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual string Area { get; set; }

        /// <summary>
        /// 是否是默认地址
        /// </summary>
        public virtual bool? IsDefault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }

}
