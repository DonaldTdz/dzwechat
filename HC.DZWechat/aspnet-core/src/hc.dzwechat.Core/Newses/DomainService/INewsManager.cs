

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.Newses;


namespace HC.DZWechat.Newses.DomainService
{
    public interface INewsManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitNews();



		 
      
         

    }
}
