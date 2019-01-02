using Abp.Application.Services;
using HC.DZWechat.Dtos;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.DZWechat.WechatMenus
{
    public interface IWechatMenuApplicationService : IApplicationService
    {
        Task<APIResultDto> CreateMenu(GetMenuResultFull fullJson);

        GetMenuResult GetMenu();
    }
}
