using Abp.Application.Services;
using Abp.Application.Services.Dto;
using hc.dzwechat.MultiTenancy.Dto;

namespace hc.dzwechat.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

