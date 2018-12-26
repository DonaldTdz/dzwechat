using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using hc.dzwechat.Authorization.Roles;
using hc.dzwechat.Authorization.Users;
using hc.dzwechat.MultiTenancy;

namespace hc.dzwechat.EntityFrameworkCore
{
    public class dzwechatDbContext : AbpZeroDbContext<Tenant, Role, User, dzwechatDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public dzwechatDbContext(DbContextOptions<dzwechatDbContext> options)
            : base(options)
        {
        }
    }
}
