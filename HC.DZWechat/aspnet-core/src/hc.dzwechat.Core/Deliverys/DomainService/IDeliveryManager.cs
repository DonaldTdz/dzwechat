

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Deliverys;


namespace HC.DZWechat.Deliverys.DomainService
{
    public interface IDeliveryManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitDelivery();



		 
      
         

    }
}
