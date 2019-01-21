

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using HC.DZWechat.VipUsers;


namespace HC.DZWechat.VipUsers.DomainService
{
    public interface IVipUserManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitVipUser();



		 
      
         

    }
}
