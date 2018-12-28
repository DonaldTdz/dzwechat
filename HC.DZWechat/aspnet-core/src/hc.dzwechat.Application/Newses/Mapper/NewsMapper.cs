
using AutoMapper;
using HC.DZWechat.Newses;
using HC.DZWechat.Newses.Dtos;

namespace HC.DZWechat.Newses.Mapper
{

	/// <summary>
    /// 配置News的AutoMapper
    /// </summary>
	internal static class NewsMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <News,NewsListDto>();
            configuration.CreateMap <NewsListDto,News>();

            configuration.CreateMap <NewsEditDto,News>();
            configuration.CreateMap <News,NewsEditDto>();

        }
	}
}
