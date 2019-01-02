

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.WechatMessages;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.WechatMessages.Dtos
{
    public class WechatMessageListDto : AuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// KeyWord
		/// </summary>
		[Required(ErrorMessage="KeyWord不能为空")]
		public string KeyWord { get; set; }



		/// <summary>
		/// MatchMode
		/// </summary>
		[Required(ErrorMessage="MatchMode不能为空")]
		public int MatchMode { get; set; }



		/// <summary>
		/// MsgType
		/// </summary>
		[Required(ErrorMessage="MsgType不能为空")]
		public MsgTypeEnum MsgType { get; set; }

        public TriggerTypeEnum TriggerType { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }


    }
}