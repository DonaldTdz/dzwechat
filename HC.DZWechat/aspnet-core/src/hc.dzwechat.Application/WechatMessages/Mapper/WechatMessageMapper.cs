
using AutoMapper;
using HC.DZWechat.WechatMessages;
using HC.DZWechat.WechatMessages.Dtos;

namespace HC.DZWechat.WechatMessages.Mapper
{

	/// <summary>
    /// 配置WechatMessage的AutoMapper
    /// </summary>
	internal static class WechatMessageMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <WechatMessage,WechatMessageListDto>();
            configuration.CreateMap <WechatMessageListDto,WechatMessage>();

            configuration.CreateMap <WechatMessageEditDto,WechatMessage>();
            configuration.CreateMap <WechatMessage,WechatMessageEditDto>();

        }
	}
}
