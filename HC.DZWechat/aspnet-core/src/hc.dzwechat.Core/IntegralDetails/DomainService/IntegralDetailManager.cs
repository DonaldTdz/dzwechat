

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
using HC.DZWechat.IntegralDetails;


namespace HC.DZWechat.IntegralDetails.DomainService
{
    /// <summary>
    /// IntegralDetail领域层的业务管理
    ///</summary>
    public class IntegralDetailManager :DZWechatDomainServiceBase, IIntegralDetailManager
    {
		
		private readonly IRepository<IntegralDetail,Guid> _repository;

		/// <summary>
		/// IntegralDetail的构造方法
		///</summary>
		public IntegralDetailManager(
			IRepository<IntegralDetail, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitIntegralDetail()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
