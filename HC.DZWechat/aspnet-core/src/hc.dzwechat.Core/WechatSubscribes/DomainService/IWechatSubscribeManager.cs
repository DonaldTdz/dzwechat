

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.WechatSubscribes;


namespace HC.DZWechat.WechatSubscribes.DomainService
{
    public interface IWechatSubscribeManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitWechatSubscribe();



		 
      
         

    }
}
