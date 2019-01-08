using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Exchanges
{
    [Table("Exchanges")]
    public class Exchange : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 订单明细Id 外键
        /// </summary>
        public virtual Guid? OrderDetailId { get; set; }
        /// <summary>
        /// 兑换方式（枚举：线下兑换、邮寄兑换）
        /// </summary>
        public virtual ExchangeCode? ExchangeCode { get; set; }
        /// <summary>
        /// 兑换店铺Id 如果有
        /// </summary>
        public virtual Guid? ShopId { get; set; }
        /// <summary>
        /// 兑换用户Id
        /// </summary>
        public virtual long? UserId { get; set; }
        /// <summary>
        /// 兑换时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        public virtual string LogisticsCompany{ get; set; }
        public virtual string LogisticsNo { get; set; }
        public virtual string UnionId { get; set; }

    }

}
