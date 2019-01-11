

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

using HC.DZWechat;
using HC.DZWechat.WechatUsers;


namespace HC.DZWechat.WechatUsers.DomainService
{
    /// <summary>
    /// WechatUser领域层的业务管理
    ///</summary>
    public class WechatUserManager : DZWechatDomainServiceBase, IWechatUserManager
    {

        private readonly IRepository<WechatUser, Guid> _repository;

        /// <summary>
        /// WechatUser的构造方法
        ///</summary>
        public WechatUserManager(
            IRepository<WechatUser, Guid> repository
        )
        {
            _repository = repository;
        }


        /// <summary>
        /// 初始化
        ///</summary>
        public void InitWechatUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取微信用户
        /// </summary>
        public async Task<WechatUser> GetWeChatUserAsync(string openId)
        {
            return await _repository.GetAll().Where(w => w.OpenId == openId).FirstOrDefaultAsync();
        }
    }
}
