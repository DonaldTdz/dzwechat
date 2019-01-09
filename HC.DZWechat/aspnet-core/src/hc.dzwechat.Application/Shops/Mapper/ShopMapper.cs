
using AutoMapper;
using HC.DZWechat.Shops;
using HC.DZWechat.Shops.Dtos;

namespace HC.DZWechat.Shops.Mapper
{

	/// <summary>
    /// 配置Shop的AutoMapper
    /// </summary>
	internal static class ShopMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Shop,ShopListDto>();
            configuration.CreateMap <ShopListDto,Shop>();

            configuration.CreateMap <ShopEditDto,Shop>();
            configuration.CreateMap <Shop,ShopEditDto>();

        }
	}
}
