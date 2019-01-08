

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Shops;


namespace HC.DZWechat.Shops.DomainService
{
    public interface IShopManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitShop();



		 
      
         

    }
}
