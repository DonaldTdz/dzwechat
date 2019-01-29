using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HC.DZWechat.Configuration;
using Abp.Runtime.Caching.Redis;

namespace HC.DZWechat.Web.Host.Startup
{
    [DependsOn(
       typeof(DZWechatWebCoreModule),
       typeof(AbpRedisCacheModule))]
    public class DZWechatWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DZWechatWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DZWechatWebHostModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.Caching.UseRedis();
        }
    }
}

