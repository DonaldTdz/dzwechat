

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
using HC.DZWechat.Shops;


namespace HC.DZWechat.Shops.DomainService
{
    /// <summary>
    /// Shop领域层的业务管理
    ///</summary>
    public class ShopManager :DZWechatDomainServiceBase, IShopManager
    {
		
		private readonly IRepository<Shop,Guid> _repository;

		/// <summary>
		/// Shop的构造方法
		///</summary>
		public ShopManager(
			IRepository<Shop, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitShop()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
