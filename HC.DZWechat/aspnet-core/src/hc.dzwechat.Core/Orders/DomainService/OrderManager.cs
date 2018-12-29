

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
using HC.DZWechat.Orders;


namespace HC.DZWechat.Orders.DomainService
{
    /// <summary>
    /// Order领域层的业务管理
    ///</summary>
    public class OrderManager :DZWechatDomainServiceBase, IOrderManager
    {
		
		private readonly IRepository<Order,Guid> _repository;

		/// <summary>
		/// Order的构造方法
		///</summary>
		public OrderManager(
			IRepository<Order, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitOrder()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
