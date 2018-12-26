using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HC.DZWechat.Authorization.Roles;
using HC.DZWechat.Authorization.Users;
using HC.DZWechat.MultiTenancy;

namespace HC.DZWechat.EntityFrameworkCore
{
    public class DZWechatDbContext : AbpZeroDbContext<Tenant, Role, User, DZWechatDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DZWechatDbContext(DbContextOptions<DZWechatDbContext> options)
            : base(options)
        {
        }
    }
}

