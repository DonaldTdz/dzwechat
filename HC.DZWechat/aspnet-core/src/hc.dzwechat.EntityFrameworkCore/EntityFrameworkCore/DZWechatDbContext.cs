using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HC.DZWechat.Authorization.Roles;
using HC.DZWechat.Authorization.Users;
using HC.DZWechat.MultiTenancy;
using HC.DZWechat.Newses;
using HC.DZWechat.WechatMessages;
using HC.DZWechat.WechatSubscribes;
using HC.DZWechat.WechatUsers;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.Orders;
using HC.DZWechat.Goods;

namespace HC.DZWechat.EntityFrameworkCore
{
    public class DZWechatDbContext : AbpZeroDbContext<Tenant, Role, User, DZWechatDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DZWechatDbContext(DbContextOptions<DZWechatDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<WechatMessage> WechatMessages { get; set; }

        public virtual DbSet<WechatSubscribe> WechatSubscribes { get; set; }
        public virtual DbSet<WechatUser> WechatUsers { get; set; }
        public virtual DbSet<IntegralDetail> IntegralDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        
    }
}

