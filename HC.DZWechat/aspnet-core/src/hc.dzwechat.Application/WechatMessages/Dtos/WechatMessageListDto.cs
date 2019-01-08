

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.WechatMessages;
using HC.DZWechat.WechatSubscribes;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.WechatMessages.Dtos
{
    public class WechatMessageListDto : AuditedEntityDto<Guid>
    {


        /// <summary>
        /// KeyWord
        /// </summary>
        [Required(ErrorMessage = "KeyWord不能为空")]
        public string KeyWord { get; set; }



        /// <summary>
        /// MatchMode
        /// </summary>
        [Required(ErrorMessage = "MatchMode不能为空")]
        public MatchModeEnum MatchMode { get; set; }



        /// <summary>
        /// MsgType
        /// </summary>
        [Required(ErrorMessage = "MsgType不能为空")]
        public MsgTypeEnum MsgType { get; set; }

        public TriggerTypeEnum TriggerType { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [Required(ErrorMessage = "Content不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicLink { get; set; }

        /// <summary>
        /// 文章连接
        /// </summary>
        public string Url { get; set; }


        public string MatchModeName
        {
            get
            {
                return MatchMode.ToString();
            }
        }

        public string MsgTypeName
        {
            get
            {
                return MsgType.ToString();
            }
        }

        public string TriggerTypeName
        {
            get
            {
                return TriggerType.ToString();
            }
        }
    }
}