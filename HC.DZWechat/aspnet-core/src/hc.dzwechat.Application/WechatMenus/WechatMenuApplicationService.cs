using Abp.Authorization;
using HC.DZWechat.Dtos;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.WechatMenus
{
    [AbpAuthorize]
    public class WechatMenuApplicationService : DZWechatAppServiceBase, IWechatMenuApplicationService
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token ?? CheckSignature.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string AppSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

        public WechatMenuApplicationService()
        {

        }

        public async Task<APIResultDto> CreateMenu(GetMenuResultFull fullJson)
        {
            try
            {
                //GetMenuResultFull resultFull = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMenuResultFull>(fullJson);
                //重新整理按钮信息
                WxJsonResult result = null;
                IButtonGroupBase buttonGroup = null;

                buttonGroup = CommonApi.GetMenuFromJsonResult(fullJson, new ButtonGroup()).menu;
                var accessToken = await AccessTokenContainer.TryGetAccessTokenAsync(AppId, AppSecret);
                result = CommonApi.CreateMenu(accessToken, buttonGroup);

                return new APIResultDto() { Code = 0, Msg = "上传菜单成功", Data = result };
            }
            catch (Exception ex)
            {
                return new APIResultDto() { Code = 901, Msg = "更新菜单失败", Data = ex.Message };
            }
        }

        public GetMenuResult GetMenu()
        { 
            return CommonApi.GetMenu(AppId);
        }

    }
}
