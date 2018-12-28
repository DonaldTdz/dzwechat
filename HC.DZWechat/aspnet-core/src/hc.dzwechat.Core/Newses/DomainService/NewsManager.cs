

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
using HC.DZWechat.Newses;


namespace HC.DZWechat.Newses.DomainService
{
    /// <summary>
    /// News领域层的业务管理
    ///</summary>
    public class NewsManager :DZWechatDomainServiceBase, INewsManager
    {
		
		private readonly IRepository<News,Guid> _repository;

		/// <summary>
		/// News的构造方法
		///</summary>
		public NewsManager(
			IRepository<News, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitNews()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
