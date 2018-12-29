using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.DZWechat.WechatSubscribes
{
    [Table("WechatSubscribes")]
    public class WechatSubscribe : AuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 消息类型（枚举 文字消息、图文消息）
        /// </summary>
        [Required]
        public virtual MsgTypeEnum MsgType { get; set; }
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

    public enum MsgTypeEnum
    {
        文字消息 = 1,
        图文消息 = 2
    }

}
