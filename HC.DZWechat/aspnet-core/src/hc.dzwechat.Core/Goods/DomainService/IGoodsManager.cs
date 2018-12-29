

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Goods;


namespace HC.DZWechat.Goods.DomainService
{
    public interface IGoodsManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitGoods();



		 
      
         

    }
}
