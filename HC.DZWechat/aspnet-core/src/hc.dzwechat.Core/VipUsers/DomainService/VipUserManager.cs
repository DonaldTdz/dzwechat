

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

using HC.DZWechat;
using HC.DZWechat.VipUsers;


namespace HC.DZWechat.VipUsers.DomainService
{
    /// <summary>
    /// VipUser领域层的业务管理
    ///</summary>
    public class VipUserManager :DZWechatDomainServiceBase, IVipUserManager
    {
		
		private readonly IRepository<VipUser,Guid> _repository;

		/// <summary>
		/// VipUser的构造方法
		///</summary>
		public VipUserManager(
			IRepository<VipUser, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitVipUser()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
