

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Categorys;


namespace HC.DZWechat.Categorys.DomainService
{
    public interface ICategoryManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitCategory();



		 
      
         

    }
}
