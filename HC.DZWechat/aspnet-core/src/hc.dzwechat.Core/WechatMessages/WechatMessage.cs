using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.DZWechat.WechatMessages
{
    [Table("WechatMessages")]
    public class WechatMessage : AuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 关键字
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string KeyWord { get; set; }
        /// <summary>
        /// 匹配模式（枚举 精准匹配、模糊匹配）
        /// </summary>
        [Required]
        public virtual int MatchMode { get; set; }
        /// <summary>
        /// 消息类型（枚举 文字消息、图文消息）
        /// </summary>
        [Required]
        public virtual int MsgType { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string Content { get; set; }
        
    }

}
