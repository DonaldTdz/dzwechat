

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.IntegralDetails;

namespace HC.DZWechat.IntegralDetails.Dtos
{
    public class CreateOrUpdateIntegralDetailInput
    {
        [Required]
        public IntegralDetailEditDto IntegralDetail { get; set; }

    }
}