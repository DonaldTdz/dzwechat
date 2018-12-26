using System.Threading.Tasks;
using hc.dzwechat.Configuration.Dto;

namespace hc.dzwechat.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
