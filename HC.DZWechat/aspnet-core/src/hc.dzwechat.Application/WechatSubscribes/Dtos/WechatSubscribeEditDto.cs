
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.WechatSubscribes;

namespace  HC.DZWechat.WechatSubscribes.Dtos
{
    public class WechatSubscribeEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// MsgType
		/// </summary>
		[Required(ErrorMessage="MsgType不能为空")]
		public MsgTypeEnum MsgType { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// CreatorUserId
		/// </summary>
		public long? CreatorUserId { get; set; }



		/// <summary>
		/// LastModificationTime
		/// </summary>
		public DateTime? LastModificationTime { get; set; }



		/// <summary>
		/// LastModifierUserId
		/// </summary>
		public long? LastModifierUserId { get; set; }




    }
}