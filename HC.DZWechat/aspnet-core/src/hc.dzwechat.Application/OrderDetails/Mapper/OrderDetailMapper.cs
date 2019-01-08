
using AutoMapper;
using HC.DZWechat.OrderDetails;
using HC.DZWechat.OrderDetails.Dtos;

namespace HC.DZWechat.OrderDetails.Mapper
{

	/// <summary>
    /// 配置OrderDetail的AutoMapper
    /// </summary>
	internal static class OrderDetailMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <OrderDetail,OrderDetailListDto>();
            configuration.CreateMap <OrderDetailListDto,OrderDetail>();

            configuration.CreateMap <OrderDetailEditDto,OrderDetail>();
            configuration.CreateMap <OrderDetail,OrderDetailEditDto>();

        }
	}
}
