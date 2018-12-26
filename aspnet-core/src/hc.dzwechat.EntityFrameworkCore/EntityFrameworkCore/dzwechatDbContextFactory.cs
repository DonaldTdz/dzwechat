using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using hc.dzwechat.Configuration;
using hc.dzwechat.Web;

namespace hc.dzwechat.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class dzwechatDbContextFactory : IDesignTimeDbContextFactory<dzwechatDbContext>
    {
        public dzwechatDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<dzwechatDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            dzwechatDbContextConfigurer.Configure(builder, configuration.GetConnectionString(dzwechatConsts.ConnectionStringName));

            return new dzwechatDbContext(builder.Options);
        }
    }
}
