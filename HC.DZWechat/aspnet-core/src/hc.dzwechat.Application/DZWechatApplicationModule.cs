using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HC.DZWechat.Authorization;

namespace HC.DZWechat
{
    [DependsOn(
        typeof(DZWechatCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DZWechatApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DZWechatAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DZWechatApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}

