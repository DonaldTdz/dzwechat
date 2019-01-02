

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Newses;
using static HC.DZWechat.DZEnums.DZEnums;

namespace HC.DZWechat.Newses.Dtos
{
    public class NewsListDto : EntityDto<Guid> 
    {

        
		/// <summary>
		/// 标题
		/// </summary>
		[Required(ErrorMessage="Title不能为空")]
		public string Title { get; set; }



		/// <summary>
		/// 作者
		/// </summary>
		[Required(ErrorMessage="Author不能为空")]
		public string Author { get; set; }



		/// <summary>
		/// 资讯类型
		/// </summary>
		public NewsType? Type { get; set; }



		/// <summary>
		/// 封面图片
		/// </summary>
		[Required(ErrorMessage="CoverPhoto不能为空")]
		public string CoverPhoto { get; set; }



		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// 发布状态
		/// </summary>
		public PushType? PushStatus { get; set; }



		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime? PushTime { get; set; }



		/// <summary>
		/// 链接类型
		/// </summary>
		public LinkType? LinkType { get; set; }



		/// <summary>
		/// 链接地址
		/// </summary>
		public string LinkAddress { get; set; }



		/// <summary>
		/// 是否删除
		/// </summary>
		[Required(ErrorMessage="IsDeleted不能为空")]
		public bool IsDeleted { get; set; }



		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// 创建人
		/// </summary>
		public long? CreatorUserId { get; set; }



		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? LastModificationTime { get; set; }



		/// <summary>
		/// 修改人
		/// </summary>
		public long? LastModifierUserId { get; set; }



		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? DeletionTime { get; set; }



		/// <summary>
		/// 删除人
		/// </summary>
		public long? DeleterUserId { get; set; }

        public string TypeName
        {
            get
            {
                return Type.ToString();
            }
        }
        public string PushStatusName
        {
            get {
                return PushStatus.ToString();
            }
        }
        public string LinkTypeName
        {
            get
            {
                return LinkType.ToString();
            }
        }


    }
}