
using AutoMapper;
using HC.DZWechat.ShopCarts;
using HC.DZWechat.ShopCarts.Dtos;

namespace HC.DZWechat.ShopCarts.Mapper
{

	/// <summary>
    /// 配置ShopCart的AutoMapper
    /// </summary>
	internal static class ShopCartMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ShopCart,ShopCartListDto>();
            configuration.CreateMap <ShopCartListDto,ShopCart>();

            configuration.CreateMap <ShopCartEditDto,ShopCart>();
            configuration.CreateMap <ShopCart,ShopCartEditDto>();

        }
	}
}
