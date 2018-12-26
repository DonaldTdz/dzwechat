using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using HC.DZWechat.Configuration;
using HC.DZWechat.Web;

namespace HC.DZWechat.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DZWechatDbContextFactory : IDesignTimeDbContextFactory<DZWechatDbContext>
    {
        public DZWechatDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DZWechatDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DZWechatDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DZWechatConsts.ConnectionStringName));

            return new DZWechatDbContext(builder.Options);
        }
    }
}

