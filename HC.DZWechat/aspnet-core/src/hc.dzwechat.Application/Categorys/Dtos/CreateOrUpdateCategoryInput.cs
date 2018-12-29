

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Categorys;

namespace HC.DZWechat.Categorys.Dtos
{
    public class CreateOrUpdateCategoryInput
    {
        [Required]
        public CategoryEditDto Category { get; set; }

    }
}