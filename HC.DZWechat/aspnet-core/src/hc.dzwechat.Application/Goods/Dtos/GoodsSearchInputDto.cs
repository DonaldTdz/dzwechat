using HC.DZWechat.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.Goods.Dtos
{
    public class GoodsSearchInputDto : WxPagedInputDto
    {
        public string KeyWord { get; set; }

        public SortTypeEnum SortType { get; set; }

        public int CategoryId { get; set; }

        public string CateCode { get; set; }
    }

    public enum SortTypeEnum
    {
        None = 0,
        ZongHe = 1, //综合
        Sale = 2,  //销量
        PriceAsc = 3,  //价格 升
        PriceDesc = 4,  //价格 降
        News = 5 //新品
    }

}
