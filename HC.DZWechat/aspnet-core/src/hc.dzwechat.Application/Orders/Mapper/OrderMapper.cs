
using AutoMapper;
using HC.DZWechat.Orders;
using HC.DZWechat.Orders.Dtos;

namespace HC.DZWechat.Orders.Mapper
{

	/// <summary>
    /// 配置Order的AutoMapper
    /// </summary>
	internal static class OrderMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Order,OrderListDto>();
            configuration.CreateMap <OrderListDto,Order>();

            configuration.CreateMap <OrderEditDto,Order>();
            configuration.CreateMap <Order,OrderEditDto>();

        }
	}
}
