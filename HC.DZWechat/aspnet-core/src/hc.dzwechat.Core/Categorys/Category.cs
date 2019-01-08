using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.DZWechat.Categorys
{
    [Table("Categorys")]
    public class Category : Entity<int>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public virtual int Seq { get; set; }
        /// <summary>
        /// 创建时间，即同步时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
