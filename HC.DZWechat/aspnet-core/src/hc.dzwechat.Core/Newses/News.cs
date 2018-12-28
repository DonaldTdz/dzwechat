using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.DZWechat.Newses
{
    [Table("Newss")]
    public class News : Entity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string Author { get; set; }
        /// <summary>
        /// 咨讯分类（枚举：营销活动、经验分享）维护在同一个页面
        /// </summary>
        public virtual int? Type { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        [StringLength(500)]
        [Required]
        public virtual string CoverPhoto { get; set; }
        /// <summary>
        /// 内容介绍
        /// </summary>
        [StringLength(500)]
        public virtual string Content { get; set; }
        /// <summary>
        /// 发布状态（枚举：草稿、已发布）
        /// </summary>
        public virtual int? PushStatus { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual DateTime? PushTime { get; set; }
        /// <summary>
        /// 连接类型 (枚举：外部连接 目前只支持外部连接页面不用显示)
        /// </summary>
        public virtual int? LinkType { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>
        [StringLength(500)]
        public virtual string LinkAddress { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long? CreatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long? DeleterUserId { get; set; }
    }

}
