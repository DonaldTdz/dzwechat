
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Newses;
using static HC.DZWechat.DZEnums.DZEnums;

namespace  HC.DZWechat.Newses.Dtos
{
    public class NewsEditDto:FullAuditedEntity<Guid?>
    {

        ///// <summary>
        ///// Id
        ///// </summary>
        //public Guid? Id { get; set; }         


        
		/// <summary>
		/// Title
		/// </summary>
		[Required(ErrorMessage="Title不能为空")]
		public string Title { get; set; }



		/// <summary>
		/// Author
		/// </summary>
		[Required(ErrorMessage="Author不能为空")]
		public string Author { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		public NewsType? Type { get; set; }



		/// <summary>
		/// CoverPhoto
		/// </summary>
		[Required(ErrorMessage="CoverPhoto不能为空")]
		public string CoverPhoto { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// PushStatus
		/// </summary>
		public PushType? PushStatus { get; set; }



		/// <summary>
		/// PushTime
		/// </summary>
		public DateTime? PushTime { get; set; }



		/// <summary>
		/// LinkType
		/// </summary>
		public LinkType? LinkType { get; set; }



		/// <summary>
		/// LinkAddress
		/// </summary>
		public string LinkAddress { get; set; }



		///// <summary>
		///// IsDeleted
		///// </summary>
		//[Required(ErrorMessage="IsDeleted不能为空")]
		//public bool IsDeleted { get; set; }



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



		///// <summary>
		///// DeletionTime
		///// </summary>
		//public DateTime? DeletionTime { get; set; }



		///// <summary>
		///// DeleterUserId
		///// </summary>
		//public long? DeleterUserId { get; set; }

        /// <summary>
        /// 发布状态名
        /// </summary>
        public string PushStatusName
        {
            get
            {
                return PushStatus.ToString();
            }
        }


    }
}