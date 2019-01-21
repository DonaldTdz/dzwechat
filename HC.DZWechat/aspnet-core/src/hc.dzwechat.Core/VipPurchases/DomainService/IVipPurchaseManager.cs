

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.VipPurchases;


namespace HC.DZWechat.VipPurchases.DomainService
{
    public interface IVipPurchaseManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitVipPurchase();



		 
      
         

    }
}
