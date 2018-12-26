using System.Threading.Tasks;
using Abp.Application.Services;
using HC.DZWechat.Authorization.Accounts.Dto;

namespace HC.DZWechat.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

