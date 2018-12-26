using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using hc.dzwechat.Authorization.Users;
using hc.dzwechat.Editions;

namespace hc.dzwechat.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
