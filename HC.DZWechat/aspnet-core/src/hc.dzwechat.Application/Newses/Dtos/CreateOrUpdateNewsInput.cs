

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Newses;

namespace HC.DZWechat.Newses.Dtos
{
    public class CreateOrUpdateNewsInput
    {
        [Required]
        public NewsEditDto News { get; set; }

    }
}