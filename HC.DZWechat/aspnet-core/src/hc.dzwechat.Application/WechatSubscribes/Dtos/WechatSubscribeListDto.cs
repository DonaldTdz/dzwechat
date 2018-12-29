

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.WechatSubscribes;

namespace HC.DZWechat.WechatSubscribes.Dtos
{
    public class WechatSubscribeListDto : AuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// MsgType
		/// </summary>
		[Required(ErrorMessage="MsgType不能为空")]
		public int MsgType { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }


    }
}