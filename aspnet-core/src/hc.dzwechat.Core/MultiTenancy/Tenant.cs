using Abp.MultiTenancy;
using hc.dzwechat.Authorization.Users;

namespace hc.dzwechat.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
