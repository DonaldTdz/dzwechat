
using AutoMapper;
using HC.DZWechat.Exchanges;
using HC.DZWechat.Exchanges.Dtos;

namespace HC.DZWechat.Exchanges.Mapper
{

	/// <summary>
    /// 配置Exchange的AutoMapper
    /// </summary>
	internal static class ExchangeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Exchange,ExchangeListDto>();
            configuration.CreateMap <ExchangeListDto,Exchange>();

            configuration.CreateMap <ExchangeEditDto,Exchange>();
            configuration.CreateMap <Exchange,ExchangeEditDto>();

        }
	}
}
