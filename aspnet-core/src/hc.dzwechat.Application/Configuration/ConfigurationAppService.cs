using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using hc.dzwechat.Configuration.Dto;

namespace hc.dzwechat.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : dzwechatAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
