

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.WechatUsers;


namespace HC.DZWechat.WechatUsers.DomainService
{
    public interface IWechatUserManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitWechatUser();
        Task<WechatUser> GetWeChatUserAsync(string openId);
    }
}
