using Abp.MultiTenancy;
using HC.DZWechat.Authorization.Users;

namespace HC.DZWechat.MultiTenancy
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

