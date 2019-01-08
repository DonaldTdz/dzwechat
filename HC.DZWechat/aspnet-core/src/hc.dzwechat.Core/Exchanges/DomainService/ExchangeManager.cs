

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
using HC.DZWechat.Exchanges;


namespace HC.DZWechat.Exchanges.DomainService
{
    /// <summary>
    /// Exchange领域层的业务管理
    ///</summary>
    public class ExchangeManager :DZWechatDomainServiceBase, IExchangeManager
    {
		
		private readonly IRepository<Exchange,Guid> _repository;

		/// <summary>
		/// Exchange的构造方法
		///</summary>
		public ExchangeManager(
			IRepository<Exchange, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitExchange()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
