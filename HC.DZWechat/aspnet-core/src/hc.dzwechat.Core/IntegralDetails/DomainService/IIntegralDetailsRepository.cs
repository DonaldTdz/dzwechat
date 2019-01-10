using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.IntegralDetails.DomainService
{
   public interface IIntegralDetailsRepository : IRepository<IntegralDetail, Guid>
    {
        /// <summary>
        /// 获取积分变化统计（按月）
        /// </summary>
        /// <returns></returns>
        Task<List<IntegralDetailDto>> GetIntegralDetailByMonthAsync(DateTime startTime, DateTime endTime);
    }
}
