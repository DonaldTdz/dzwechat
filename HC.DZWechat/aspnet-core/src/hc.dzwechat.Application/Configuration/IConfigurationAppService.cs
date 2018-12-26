using System.Threading.Tasks;
using HC.DZWechat.Configuration.Dto;

namespace HC.DZWechat.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

