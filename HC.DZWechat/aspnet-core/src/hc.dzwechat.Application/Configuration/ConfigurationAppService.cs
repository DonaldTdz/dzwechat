using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using HC.DZWechat.Configuration.Dto;

namespace HC.DZWechat.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DZWechatAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

