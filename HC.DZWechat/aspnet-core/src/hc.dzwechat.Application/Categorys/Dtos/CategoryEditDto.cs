
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using HC.DZWechat.Categorys;

namespace HC.DZWechat.Categorys.Dtos
{
    public class CategoryEditDto : EntityDto<int?>, IHasCreationTime
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Name不能为空")]
        public string Name { get; set; }



        /// <summary>
        /// Seq
        /// </summary>
        [Required(ErrorMessage = "Seq不能为空")]
        public int Seq { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }
    }
}