

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.OrderDetails;


namespace HC.DZWechat.OrderDetails.DomainService
{
    public interface IOrderDetailManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitOrderDetail();



		 
      
         

    }
}
