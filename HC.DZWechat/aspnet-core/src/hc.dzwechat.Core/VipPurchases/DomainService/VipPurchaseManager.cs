

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
using HC.DZWechat.VipPurchases;


namespace HC.DZWechat.VipPurchases.DomainService
{
    /// <summary>
    /// VipPurchase领域层的业务管理
    ///</summary>
    public class VipPurchaseManager :DZWechatDomainServiceBase, IVipPurchaseManager
    {
		
		private readonly IRepository<VipPurchase,Guid> _repository;

		/// <summary>
		/// VipPurchase的构造方法
		///</summary>
		public VipPurchaseManager(
			IRepository<VipPurchase, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitVipPurchase()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
