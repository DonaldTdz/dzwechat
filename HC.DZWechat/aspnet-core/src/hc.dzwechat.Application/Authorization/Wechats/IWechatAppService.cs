using Abp.Application.Services;
using HC.DZWechat.Dtos;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.Authorization.Wechats
{
    public interface IWechatAppService : IApplicationService
    {
        Task<JsCode2JsonResult> GetJsCode2Session(string jsCode, string nickName);
    }
}
