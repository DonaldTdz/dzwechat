

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Newses;
using HC.DZWechat.DZEnums.DZCommonEnums;

namespace HC.DZWechat.Newses.Dtos
{
    public class NewsListDto : FullAuditedEntityDto<Guid> 
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