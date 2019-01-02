using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.WechatSubscribes;
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
        public virtual MsgTypeEnum MsgType { get; set; }

        /// <summary>
        /// 触发类型 （枚举 关键字、点击事件）
        /// </summary>
        public virtual TriggerTypeEnum TriggerType { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Content { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Desc { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public virtual string PicLink { get; set; }

        /// <summary>
        /// 文章连接
        /// </summary>
        public virtual string Url { get; set; }
    }

    /// <summary>
    /// 触发类型
    /// </summary>
    public enum TriggerTypeEnum
    {
        关键字 = 1,
        点击事件 = 2
    }

}
