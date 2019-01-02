

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Orders;


namespace HC.DZWechat.Orders.DomainService
{
    public interface IOrderManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitOrder();



		 
      
         

    }
}
