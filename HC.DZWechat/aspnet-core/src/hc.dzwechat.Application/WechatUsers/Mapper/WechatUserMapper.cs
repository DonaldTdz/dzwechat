
using AutoMapper;
using HC.DZWechat.WechatUsers;
using HC.DZWechat.WechatUsers.Dtos;

namespace HC.DZWechat.WechatUsers.Mapper
{

	/// <summary>
    /// 配置WechatUser的AutoMapper
    /// </summary>
	internal static class WechatUserMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <WechatUser,WechatUserListDto>();
            configuration.CreateMap <WechatUserListDto,WechatUser>();

            configuration.CreateMap <WechatUserEditDto,WechatUser>();
            configuration.CreateMap <WechatUser,WechatUserEditDto>();

        }
	}
}
