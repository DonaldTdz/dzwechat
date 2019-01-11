

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.ShopCarts;


namespace HC.DZWechat.ShopCarts.DomainService
{
    public interface IShopCartManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitShopCart();



		 
      
         

    }
}
