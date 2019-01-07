using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using HC.DZWechat.Dtos;
using Senparc.Weixin;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

namespace HC.DZWechat.Authorization.Wechats
{
    public class WechatAppService : DZWechatAppServiceBase, IWechatAppService
    {
        //小程序
        private string wxappId = Config.SenparcWeixinSetting.WxOpenAppId;
        private string wxappSecret = Config.SenparcWeixinSetting.WxOpenAppSecret;

        [AbpAllowAnonymous]
        public async Task<JsCode2JsonResult> GetJsCode2Session(string jsCode, string nickName)
        {
            var result = await SnsApi.JsCode2JsonAsync(wxappId, wxappSecret, jsCode);
            return result;
        }
    }
}
