

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Orders;
using HC.DZWechat.DZEnums.DZCommonEnums;
using Abp.AutoMapper;

namespace HC.DZWechat.Orders.Dtos
{
    [AutoMapFrom(typeof(Order))]

    public class OrderListDto : EntityDto<Guid>, IHasCreationTime, ICreationAudited
    {


        /// <summary>
        /// Number
        /// </summary>
        [Required(ErrorMessage = "Number不能为空")]
        public string Number { get; set; }
        public string NickName { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public Guid? UserId { get; set; }



        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }



        /// <summary>
        /// Integral
        /// </summary>
        public decimal? Integral { get; set; }



        /// <summary>
        /// Status
        /// </summary>
        public OrderStatus? Status { get; set; }

        public string StatusName
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }



        /// <summary>
        /// PayTime
        /// </summary>
        public DateTime? PayTime { get; set; }



        /// <summary>
        /// CompleteTime
        /// </summary>
        public DateTime? CompleteTime { get; set; }



        /// <summary>
        /// CancelTime
        /// </summary>
        public DateTime? CancelTime { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }



        /// <summary>
        /// CreatorUserId
        /// </summary>
        public long? CreatorUserId { get; set; }



        /// <summary>
        /// DeliveryName
        /// </summary>
        public string DeliveryName { get; set; }



        /// <summary>
        /// DeliveryPhone
        /// </summary>
        public string DeliveryPhone { get; set; }



        /// <summary>
        /// DeliveryAddress
        /// </summary>
        public string DeliveryAddress { get; set; }

        public string UserName { get; set; }
    }
    public class HomeInfo
    {
        /// <summary>
        /// 会员数
        /// </summary>
        public int WeChatUsersTotal { get; set; }

        /// <summary>
        /// 积分数
        /// </summary>
        public int IntegralTotal { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderTotal { get; set; }

        /// <summary>
        /// 待处理订单数
        /// </summary>
        public int PendingOrderTotal { get; set; }
    }

}