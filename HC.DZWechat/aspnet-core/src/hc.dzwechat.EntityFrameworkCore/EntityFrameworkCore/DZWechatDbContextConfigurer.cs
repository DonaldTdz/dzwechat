using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HC.DZWechat.EntityFrameworkCore
{
    public static class DZWechatDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DZWechatDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DZWechatDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseMySql(connection);
        }
    }
}

