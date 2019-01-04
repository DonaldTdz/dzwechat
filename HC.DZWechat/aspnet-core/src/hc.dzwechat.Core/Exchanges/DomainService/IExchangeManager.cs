

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Exchanges;


namespace HC.DZWechat.Exchanges.DomainService
{
    public interface IExchangeManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitExchange();



		 
      
         

    }
}
