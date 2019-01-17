
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
            configuration.CreateMap <ShopGoods,GoodListDto>();
            configuration.CreateMap <GoodListDto,ShopGoods>();

            configuration.CreateMap <GoodEditDto,ShopGoods>();
            configuration.CreateMap <ShopGoods,GoodEditDto>();

        }
	}
}
