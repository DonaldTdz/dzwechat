using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HC.DZWechat.WechatConfigs
{
    [Table("WechatConfigs")]
    public class WechatConfig : Entity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 模板Id
        /// </summary>
        [StringLength(200)]
        public virtual string TemplateIds { get; set; }
        /// <summary>
        /// 管理员WxOpenId
        /// </summary>
        [StringLength(200)]
        public virtual string ManagerWxOpenId { get; set; }
    }
}
