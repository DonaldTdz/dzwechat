

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HC.DZWechat.VipUsers;

namespace HC.DZWechat.VipUsers.Dtos
{
    public class CreateOrUpdateVipUserInput
    {
        [Required]
        public VipUserEditDto VipUser { get; set; }

    }
}