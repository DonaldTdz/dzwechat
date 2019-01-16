using HC.DZWechat.Deliverys.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.ShopCarts.Dtos
{
    public class PayOrderDto
    {
        //1. 收货地址信息
        public bool IsExitAddress { get; set; }
        public DeliveryListDto DefaultAddress { get; set; }

        //2. 购物车商品信息
        public List<UserCartDto> Items { get; set; }

        //3. 用户积分信息
        public decimal? Integral { get; set; }

        //4. 支付积分信息
        public decimal? TotalPrice { get; set; }
    }
}
