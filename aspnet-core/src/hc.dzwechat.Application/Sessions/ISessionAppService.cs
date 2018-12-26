using System.Threading.Tasks;
using Abp.Application.Services;
using hc.dzwechat.Sessions.Dto;

namespace hc.dzwechat.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
