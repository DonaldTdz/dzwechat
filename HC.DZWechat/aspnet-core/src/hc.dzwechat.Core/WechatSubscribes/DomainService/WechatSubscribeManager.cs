

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
using HC.DZWechat.WechatSubscribes;


namespace HC.DZWechat.WechatSubscribes.DomainService
{
    /// <summary>
    /// WechatSubscribe领域层的业务管理
    ///</summary>
    public class WechatSubscribeManager :DZWechatDomainServiceBase, IWechatSubscribeManager
    {
		
		private readonly IRepository<WechatSubscribe,Guid> _repository;

		/// <summary>
		/// WechatSubscribe的构造方法
		///</summary>
		public WechatSubscribeManager(
			IRepository<WechatSubscribe, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitWechatSubscribe()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
