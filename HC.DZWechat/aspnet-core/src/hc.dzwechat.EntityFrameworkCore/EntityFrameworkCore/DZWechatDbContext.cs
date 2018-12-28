using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HC.DZWechat.Authorization.Roles;
using HC.DZWechat.Authorization.Users;
using HC.DZWechat.MultiTenancy;
using HC.DZWechat.WechatMessages;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.EntityFrameworkCore
{
    public class DZWechatDbContext : AbpZeroDbContext<Tenant, Role, User, DZWechatDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DZWechatDbContext(DbContextOptions<DZWechatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WechatMessage> WechatMessages { get; set; }

        public virtual DbSet<WechatSubscribe> WechatSubscribes { get; set; }
    }
}

