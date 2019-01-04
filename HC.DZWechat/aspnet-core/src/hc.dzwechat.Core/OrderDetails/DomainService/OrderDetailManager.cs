

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
using HC.DZWechat.OrderDetails;


namespace HC.DZWechat.OrderDetails.DomainService
{
    /// <summary>
    /// OrderDetail领域层的业务管理
    ///</summary>
    public class OrderDetailManager :DZWechatDomainServiceBase, IOrderDetailManager
    {
		
		private readonly IRepository<OrderDetail,Guid> _repository;

		/// <summary>
		/// OrderDetail的构造方法
		///</summary>
		public OrderDetailManager(
			IRepository<OrderDetail, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitOrderDetail()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
