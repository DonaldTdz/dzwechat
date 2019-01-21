

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HC.DZWechat.VipPurchases;

namespace HC.DZWechat.EntityMapper.VipPurchases
{
    public class VipPurchaseCfg : IEntityTypeConfiguration<VipPurchase>
    {
        public void Configure(EntityTypeBuilder<VipPurchase> builder)
        {

            builder.ToTable("VipPurchases", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.VipUserId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.VipCode).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PurchaseAmount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IsConvert).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ConvertTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


