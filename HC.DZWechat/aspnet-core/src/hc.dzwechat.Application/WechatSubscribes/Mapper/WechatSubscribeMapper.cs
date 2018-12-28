
using AutoMapper;
using HC.DZWechat.WechatSubscribes;
using HC.DZWechat.WechatSubscribes.Dtos;

namespace HC.DZWechat.WechatSubscribes.Mapper
{

	/// <summary>
    /// 配置WechatSubscribe的AutoMapper
    /// </summary>
	internal static class WechatSubscribeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <WechatSubscribe,WechatSubscribeListDto>();
            configuration.CreateMap <WechatSubscribeListDto,WechatSubscribe>();

            configuration.CreateMap <WechatSubscribeEditDto,WechatSubscribe>();
            configuration.CreateMap <WechatSubscribe,WechatSubscribeEditDto>();

        }
	}
}
