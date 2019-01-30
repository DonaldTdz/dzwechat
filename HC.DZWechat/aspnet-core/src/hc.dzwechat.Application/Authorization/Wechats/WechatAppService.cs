using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using HC.DZWechat.Dtos;
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.IntegralDetails;
using HC.DZWechat.WechatUsers;
using Microsoft.EntityFrameworkCore;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

namespace HC.DZWechat.Authorization.Wechats
{
    public class WechatAppService : DZWechatAppServiceBase, IWechatAppService
    {
        //小程序
        private string wxappId = Config.SenparcWeixinSetting.WxOpenAppId;
        private string wxappSecret = Config.SenparcWeixinSetting.WxOpenAppSecret;
        //公众号
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;

        private readonly IRepository<WechatUser, Guid> _wechatUserRepository;
        private readonly IRepository<IntegralDetail, Guid> _integralDetailRepository;

        public WechatAppService(IRepository<WechatUser, Guid> wechatUserRepository,
            IRepository<IntegralDetail, Guid> integralDetailRepository
            )
        {
            _wechatUserRepository = wechatUserRepository;
            _integralDetailRepository = integralDetailRepository;
        }

        [AbpAllowAnonymous]
        public async Task<JsCode2JsonResult> GetJsCode2Session(string jsCode, string nickName)
        {
            var result = await SnsApi.JsCode2JsonAsync(wxappId, wxappSecret, jsCode);
            await AddWxUser(result, nickName);
            return result;
        }

        private async Task AddWxUser(JsCode2JsonResult result, string nickName)
        {
            try
            {
                //是否已经绑定小程序
                var user = await _wechatUserRepository.GetAll().Where(w => w.WxOpenId == result.openid).FirstOrDefaultAsync();
                if (user == null && !string.IsNullOrEmpty(result.unionid))
                {
                    //是否绑定开放平台公众号
                    user = await _wechatUserRepository.GetAll().Where(w => w.UnionId == result.unionid).FirstOrDefaultAsync();
                }
               
                if (user == null)//新增
                {
                    WechatUser wechatUser = new WechatUser();
                    wechatUser.BindStatus = BindStatus.已关注;
                    wechatUser.BindTime = DateTime.Now;
                    //wechatUser.HeadImgUrl = wxuser.headimgurl;
                    wechatUser.Integral = 10;//首次关注获取10积分
                    wechatUser.IsShopManager = false;
                    wechatUser.NickName = nickName;
                    //wechatUser.OpenId = wxuser.openid;
                    wechatUser.WxOpenId = result.openid;
                    wechatUser.UnionId = result.unionid;
                    wechatUser.UserType = UserType.普通会员;
                    var uid = await _wechatUserRepository.InsertAndGetIdAsync(wechatUser);
                    await CurrentUnitOfWork.SaveChangesAsync();

                    IntegralDetail integralDetail = new IntegralDetail();
                    integralDetail.InitialIntegral = 0;
                    integralDetail.Integral = 10;
                    integralDetail.FinalIntegral = 10;
                    integralDetail.Type = IntegralType.绑定会员;
                    integralDetail.UserId = uid;
                    integralDetail.Desc = "首次绑定小程序";
                    await _integralDetailRepository.InsertAsync(integralDetail);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
                else if(string.IsNullOrEmpty(user.WxOpenId))//更新
                {
                    //user.BindStatus = BindStatus.已关注;
                    //user.HeadImgUrl = wxuser.headimgurl;
                    user.WxOpenId = result.openid;
                    user.NickName = nickName;
                    //user.BindTime = DateTime.Now;
                    user.UnionId = result.unionid;
                    await _wechatUserRepository.UpdateAsync(user);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("小程序授权保存用户异常：{0}", ex);
            }
        }
    }
}
