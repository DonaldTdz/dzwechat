
using Abp.Runtime.Validation;
using HC.DZWechat.Dtos;
using HC.DZWechat.WechatMessages;
using static HC.DZWechat.DZEnums.DZEnums;

namespace HC.DZWechat.WechatMessages.Dtos
{
    public class GetWechatMessagesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string MesText { get; set; }

        /// <summary>
        /// 触发类型
        /// </summary>
        public TriggerTypeEnum? TriggerType { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgTypeEnum? MsgType { get; set; }


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
}
