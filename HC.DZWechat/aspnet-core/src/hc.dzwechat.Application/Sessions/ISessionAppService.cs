using System.Threading.Tasks;
using Abp.Application.Services;
using HC.DZWechat.Sessions.Dto;

namespace HC.DZWechat.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

