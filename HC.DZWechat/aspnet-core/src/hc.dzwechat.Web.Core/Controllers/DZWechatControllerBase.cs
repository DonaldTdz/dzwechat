using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace HC.DZWechat.Controllers
{
    public abstract class DZWechatControllerBase: AbpController
    {
        protected DZWechatControllerBase()
        {
            LocalizationSourceName = DZWechatConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

