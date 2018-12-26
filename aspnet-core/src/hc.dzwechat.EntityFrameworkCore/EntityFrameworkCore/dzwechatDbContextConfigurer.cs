using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace hc.dzwechat.EntityFrameworkCore
{
    public static class dzwechatDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<dzwechatDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<dzwechatDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
