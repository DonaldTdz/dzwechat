
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.WechatSubscribes;
using HC.DZWechat.DZEnums.DZCommonEnums;
using Abp.Application.Services.Dto;

namespace  HC.DZWechat.WechatSubscribes.Dtos
{
    public class WechatSubscribeEditDto:AuditedEntityDto<Guid?>
    {

        ///// <summary>
        ///// Id
        ///// </summary>
        //public Guid? Id { get; set; }         


        
		/// <summary>
		/// MsgType
		/// </summary>
		[Required(ErrorMessage="MsgType不能为空")]
		public MsgTypeEnum MsgType { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		//[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Desc { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public virtual string PicLink { get; set; }

        /// <summary>
        /// 文章连接
        /// </summary>
        public virtual string Url { get; set; }

        ///// <summary>
        ///// CreationTime
        ///// </summary>
        //[Required(ErrorMessage="CreationTime不能为空")]
        //public DateTime CreationTime { get; set; }



        ///// <summary>
        ///// CreatorUserId
        ///// </summary>
        //public long? CreatorUserId { get; set; }



        ///// <summary>
        ///// LastModificationTime
        ///// </summary>
        //public DateTime? LastModificationTime { get; set; }



        ///// <summary>
        ///// LastModifierUserId
        ///// </summary>
        //public long? LastModifierUserId { get; set; }




    }
}