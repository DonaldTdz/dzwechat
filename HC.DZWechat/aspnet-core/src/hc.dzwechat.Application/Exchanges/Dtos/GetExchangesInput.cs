
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.DZEnums.DZCommonEnums;
using HC.DZWechat.Exchanges;
using System;

namespace HC.DZWechat.Exchanges.Dtos
{
    public class GetExchangesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid ShopId { get; set; }
        public string ShopType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }

    public class ExchangeInput: PagedSortedAndFilteredInputDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string  OrderId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public Guid? ShopId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 兑换方式
        /// </summary>
        public ExchangeCodeEnum? ExchangeStyle { get; set; }


        public DateTime? EndTimeAddOne
        {
            get
            {
                if (EndTime.HasValue)
                {
                    return EndTime.Value.AddDays(1);
                }
                return EndTime;
            }
        }

    }
}
