using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.IntegralDetails
{
    public class IntegralDetailDto
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 增长积分
        /// </summary>
        public int? GrowIntegral { get; set; }
        /// <summary>
        /// 消耗积分
        /// </summary>
        public int? DepleteIntegral { get; set; }
    }
}
