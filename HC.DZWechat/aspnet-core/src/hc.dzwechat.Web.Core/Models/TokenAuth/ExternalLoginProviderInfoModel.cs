using Abp.AutoMapper;
using HC.DZWechat.Authentication.External;

namespace HC.DZWechat.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

