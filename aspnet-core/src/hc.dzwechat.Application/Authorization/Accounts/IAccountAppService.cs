using System.Threading.Tasks;
using Abp.Application.Services;
using hc.dzwechat.Authorization.Accounts.Dto;

namespace hc.dzwechat.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
