using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Goods
{
    [Table("Goods")]
    public class Good : Entity<Guid>, IHasCreationTime, ICreationAudited //注意修改主键Id数据类型
    {
        /// <summary>
        /// 商品规格名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Specification { get; set; }
        /// <summary>
        /// 商品图片多图（多图已逗号分隔）
        /// </summary>
        [StringLength(500)]
        public virtual string PhotoUrl { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [StringLength(500)]
        public virtual string Desc { get; set; }
        /// <summary>
        /// 库存 （不填 将不做限制）
        /// </summary>
        public virtual decimal? Stock { get; set; }
        /// <summary>
        /// 商品单位
        /// </summary>
        [StringLength(30)]
        public virtual string Unit { get; set; }
        /// <summary>
        /// 兑换方式 多选（枚举：线下兑换、邮寄兑换）多个逗号分隔
        /// </summary>
        [StringLength(50)]
        public virtual string ExchangeCode { get; set; }
        /// <summary>
        /// 商品分类 外键
        /// </summary>
        public virtual int? CategoryId { get; set; }
        /// <summary>
        /// 兑换所需积分
        /// </summary>
        public virtual decimal? Integral { get; set; }
        /// <summary>
        /// 条码 保留 暂不维护
        /// </summary>
        [StringLength(50)]
        public virtual string BarCode { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public virtual int? SellCount { get; set; }
        /// <summary>
        /// 上架、下架
        /// </summary>
        public virtual bool? IsAction { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        public virtual DateTime OnlineTime { get; set; }
        public virtual DateTime OfflineTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual long? CreatorUserId { get; set; }
    }

}
