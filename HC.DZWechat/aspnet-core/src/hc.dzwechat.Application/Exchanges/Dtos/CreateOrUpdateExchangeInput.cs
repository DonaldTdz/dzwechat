

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.Exchanges;

namespace HC.DZWechat.Exchanges.Dtos
{
    public class CreateOrUpdateExchangeInput
    {
        [Required]
        public ExchangeEditDto Exchange { get; set; }

    }
}