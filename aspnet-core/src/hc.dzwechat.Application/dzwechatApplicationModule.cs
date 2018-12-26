using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using hc.dzwechat.Authorization;

namespace hc.dzwechat
{
    [DependsOn(
        typeof(dzwechatCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class dzwechatApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<dzwechatAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(dzwechatApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
