

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.EntityMapper.WechatSubscribes
{
    public class WechatSubscribeCfg : IEntityTypeConfiguration<WechatSubscribe>
    {
        public void Configure(EntityTypeBuilder<WechatSubscribe> builder)
        {

            builder.ToTable("WechatSubscribes", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.MsgType).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Content).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreatorUserId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LastModificationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LastModifierUserId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


