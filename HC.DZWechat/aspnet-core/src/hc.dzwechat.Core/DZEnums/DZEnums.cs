using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.DZEnums
{
    public class DZEnums
    {
        public enum NewsType
        {
            烟语课堂 = 1,
            新品快讯 = 2,
            产品大全 = 3,
        }

        public enum UserType
        {
            普通会员 = 1,
            Vip会员 = 2
        }

        public enum BindStatus
        {
            已绑定 = 1,
            未绑定 = 2
        }

        public enum IntegralType
        {
            绑定会员 = 1,
            每日签到 = 2,
            VIP系统导入 = 3,
            商品兑换 = 4
        }

        public enum OrderStatus
        {
            待支付 = 1,
            已支付 = 2,
            已完成 = 3,
            已取消 = 4
        }
    }
}
