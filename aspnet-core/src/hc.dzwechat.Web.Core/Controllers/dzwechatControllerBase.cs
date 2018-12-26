using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace hc.dzwechat.Controllers
{
    public abstract class dzwechatControllerBase: AbpController
    {
        protected dzwechatControllerBase()
        {
            LocalizationSourceName = dzwechatConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
