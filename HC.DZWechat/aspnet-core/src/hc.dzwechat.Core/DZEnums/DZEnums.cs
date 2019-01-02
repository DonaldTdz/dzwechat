using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.DZEnums
{
    public class DZEnums
    {
        /// <summary>
        /// 资讯类型
        /// </summary>
        public enum NewsType
        {
            烟语课堂=1,
            新品快讯 =2,
            产品大全 =3,
        }

        /// <summary>
        /// 发布状态
        /// </summary>
        public enum PushType
        {
            草稿=0,
            已发布=1,
        }

        /// <summary>
        /// 链接类型
        /// </summary>
        public enum LinkType
        {
            外部链接=1,
        }
    }
}
