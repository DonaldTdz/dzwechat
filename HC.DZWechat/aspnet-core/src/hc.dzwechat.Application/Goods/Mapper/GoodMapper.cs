
using AutoMapper;
using HC.DZWechat.Goods;
using HC.DZWechat.Goods.Dtos;

namespace HC.DZWechat.Goods.Mapper
{

	/// <summary>
    /// 配置Good的AutoMapper
    /// </summary>
	internal static class GoodMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Good,GoodListDto>();
            configuration.CreateMap <GoodListDto,Good>();

            configuration.CreateMap <GoodEditDto,Good>();
            configuration.CreateMap <Good,GoodEditDto>();

        }
	}
}
