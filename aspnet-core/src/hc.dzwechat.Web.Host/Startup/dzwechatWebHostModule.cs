using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using hc.dzwechat.Configuration;

namespace hc.dzwechat.Web.Host.Startup
{
    [DependsOn(
       typeof(dzwechatWebCoreModule))]
    public class dzwechatWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public dzwechatWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(dzwechatWebHostModule).GetAssembly());
        }
    }
}
