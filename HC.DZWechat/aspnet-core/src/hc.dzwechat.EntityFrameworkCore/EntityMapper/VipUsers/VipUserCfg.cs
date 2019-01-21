

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HC.DZWechat.VipUsers;

namespace HC.DZWechat.EntityMapper.VipUsers
{
    public class VipUserCfg : IEntityTypeConfiguration<VipUser>
    {
        public void Configure(EntityTypeBuilder<VipUser> builder)
        {

            builder.ToTable("VipUsers", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.VipCode).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Name).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Phone).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IdNumber).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PurchaseAmount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


