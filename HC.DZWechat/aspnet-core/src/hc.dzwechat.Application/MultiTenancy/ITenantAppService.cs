using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HC.DZWechat.MultiTenancy.Dto;

namespace HC.DZWechat.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


